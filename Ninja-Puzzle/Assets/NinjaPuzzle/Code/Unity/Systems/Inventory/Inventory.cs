﻿using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using NinjaPuzzle.Code.Unity.ScriptableObjects.Inventory;

namespace NinjaPuzzle.Code.Unity.Systems.Inventory
{
	public sealed class Inventory
	{
		public int Key { get; private set; }
		public ItemStack[] Stacks { get; private set; }
		public Action OnChange;

		public Inventory(int maxStacks)
		{
			Key = this.GetHashCode();
			Stacks = new ItemStack[maxStacks];
		}

#region Public Methods

		public void Clear()
		{
			Stacks = new ItemStack[Stacks.Length];
			OnChange?.Invoke();
		}

		public ItemStack SafeAddToIndex(ItemStack itemStack, int stackIndex)
		{
			ItemStack returnItemStack = itemStack;
			
			if (stackIndex < Stacks.Length)
			{
				if (!Stacks[stackIndex].ItemData)
				{
					Stacks[stackIndex] = itemStack;
					returnItemStack = new ItemStack();
				}
				else if (Stacks[stackIndex].ItemData == itemStack.ItemData)
				{
					int itemRemains = Stacks[stackIndex].Add((uint)itemStack.Count);
					returnItemStack = new ItemStack(itemStack.ItemData, (uint) itemRemains);
				}
			}
			
			OnChange?.Invoke();
			
			return returnItemStack;
		}
		
		public ItemStack SafeUseFromIndex(int stackIndex, int count = -1)
		{
			ItemStack itemStack = Stacks[stackIndex];
			if (count <= 0)
			{
				Stacks[stackIndex] = new ItemStack();
			}
			else
			{
				int itemRemains = Stacks[stackIndex].Get((uint) count);
				if (itemRemains > 0 || itemRemains < 0)
				{
					Stacks[stackIndex] = new ItemStack();
				}
				else
				{
					itemStack = new ItemStack(Stacks[stackIndex].ItemData, (uint) count);
				}
			}
			OnChange?.Invoke();
			return itemStack;
		}

		public List<ItemData> SafeAdd(ItemData itemData, uint count)
		{
			DOVirtual.DelayedCall(0, () => OnChange?.Invoke());
			return SafeAddRecursive(itemData, count);
		}

		private List<ItemData> SafeAddRecursive(ItemData itemData, uint count)
		{
			List<ItemData> remainsItems = new List<ItemData>();

			int stackIndex = FindNotFilledStack(itemData);
			
			if (stackIndex > -1 && Stacks[stackIndex].Count < Stacks[stackIndex].ItemData.MaxItemsInStack)
			{
				int itemRemains = Stacks[stackIndex].Add(count);
				if (itemRemains > 0)
				{
					int freeIndex = GetFreeIndex();
					if (GetStacksCount() < Stacks.Length && freeIndex > -1)
					{
						Stacks[freeIndex] = new ItemStack(itemData, 0);
					}
					return SafeAddRecursive(itemData, (uint) itemRemains);
				}
			}
			else if (GetStacksCount() < Stacks.Length)
			{
				ItemStack newStack = new ItemStack(itemData, 0);
				int itemRemains = newStack.Add(count);

				int freeIndex = GetFreeIndex();
				if (freeIndex > -1)
				{
					Stacks[freeIndex] = newStack;
				}
				
				if (itemRemains > 0)
				{
					return SafeAddRecursive(itemData, (uint) itemRemains);
				}
			}
			else
			{
				remainsItems.AddRange(Enumerable.Repeat(itemData, (int)count).ToList());
			}

			return remainsItems;
		}

		public List<ItemData> SafeUse(ItemData itemData, uint count)
		{
			DOVirtual.DelayedCall(0, () => OnChange?.Invoke());
			return SafeAddRecursive(itemData, count);
		}

		private List<ItemData> SafeUseRecursive(ItemData itemData, uint count)
		{
			List<ItemData> itemsToUse = new List<ItemData>();
			
			int stackIndex = FindStackIndex(itemData);
			
			if (stackIndex > -1)
			{
				ItemStack foundedStack = Stacks[stackIndex];
				int itemRemains = foundedStack.Get(count);
				
				if (itemRemains > 0 || itemRemains < 0)
				{
					Stacks[stackIndex] = new ItemStack();
				}

				if (itemRemains > 0)
				{
					itemsToUse.AddRange(Enumerable.Repeat(itemData, (int)(count - itemRemains)).ToList());
					itemsToUse.AddRange(SafeUseRecursive(itemData, (uint) itemRemains));
				}
				else
				{
					itemsToUse.AddRange(Enumerable.Repeat(itemData, (int)count).ToList());
				}
			}

			return itemsToUse;
		}
		
#endregion

#region Helper Methods

		private int GetStacksCount()
		{
			return Stacks.Count(t => t.ItemData != null);
		}

		private int GetFreeIndex()
		{
			for (int i = 0; i < Stacks.Length; i++)
			{
				if (Stacks[i].ItemData == null)
				{
					return i;
				}
			}

			return -1;
		}

		private int FindStackIndex(ItemData itemData)
		{
			for (var i = 0; i < Stacks.Length; i++)
			{
				if (Stacks[i].ItemData && Stacks[i].ItemData == itemData)
				{
					return i;
				}
			}

			return -1;
		}

		private int FindNotFilledStack(ItemData itemData)
		{
			for (int i = 0; i < Stacks.Length; i++)
			{
				if (Stacks[i].ItemData && Stacks[i].ItemData == itemData && Stacks[i].Count < itemData.MaxItemsInStack)
				{
					return i;
				}
			}

			return -1;
		}

#endregion
	}
}