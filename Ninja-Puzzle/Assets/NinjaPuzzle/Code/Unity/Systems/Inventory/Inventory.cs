﻿using System.Collections.Generic;
using System.Linq;
using NinjaPuzzle.Code.Unity.ScriptableObjects.Inventory;

namespace NinjaPuzzle.Code.Unity.Systems.Inventory
{
	public class Inventory
	{
		public ItemStack[] Stacks { get; private set; }

		public Inventory(int maxStacks)
		{
			Stacks = new ItemStack[maxStacks];
		}

#region Public Methods

		public void Clear()
		{
			Stacks = new ItemStack[Stacks.Length];
		}

		public ItemStack SafeAddToIndex(ItemStack itemStack, int stackIndex)
		{
			if (stackIndex < Stacks.Length)
			{
				if (Stacks[stackIndex] == null)
				{
					Stacks[stackIndex] = itemStack;
					return null;
				}
				if (Stacks[stackIndex].ItemData == itemStack.ItemData)
				{
					int itemRemains = Stacks[stackIndex].Add((uint)itemStack.Count);
					return new ItemStack(itemStack.ItemData, (uint) itemRemains);
				}
			}
			
			return itemStack;
		}
		
		public ItemStack SafeUseFromIndex(int stackIndex)
		{
			ItemStack itemStack = Stacks[stackIndex];
			Stacks[stackIndex] = null;
			return itemStack;
		}

		public List<ItemData> SafeAdd(ItemData itemData, uint count)
		{
			List<ItemData> remainsItems = new List<ItemData>();

			ItemStack foundedStack = null;
			
			int stackIndex = FindNotFilledStack(itemData);
			
			if (stackIndex > -1)
			{
				foundedStack = Stacks[stackIndex];
			}

			if (foundedStack != null)
			{
				int itemRemains = foundedStack.Add(count);
				if (itemRemains > 0)
				{
					int freeIndex = GetFreeIndex();
					if (GetStacksCount() < Stacks.Length && freeIndex > -1)
					{
						Stacks[freeIndex] = new ItemStack(itemData, 0);
					}
					return SafeAdd(itemData, (uint) itemRemains);
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
					return SafeAdd(itemData, (uint) itemRemains);
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
			List<ItemData> itemsToUse = new List<ItemData>();
			
			int stackIndex = FindStackIndex(itemData);
			
			if (stackIndex > -1)
			{
				ItemStack foundedStack = Stacks[stackIndex];
				int itemRemains = foundedStack.Get(count);
				
				if (itemRemains > 0 || itemRemains < 0)
				{
					Stacks[stackIndex] = null;
				}

				if (itemRemains > 0)
				{
					itemsToUse.AddRange(Enumerable.Repeat(itemData, (int)(count - itemRemains)).ToList());
					itemsToUse.AddRange(SafeUse(itemData, (uint) itemRemains));
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
			return Stacks.Count(t => t != null);
		}

		private int GetFreeIndex()
		{
			for (int i = 0; i < Stacks.Length; i++)
			{
				if (Stacks[i] == null)
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
				if (Stacks[i] != null && Stacks[i].ItemData == itemData)
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
				if (Stacks[i] != null && Stacks[i].ItemData == itemData && Stacks[i].Count < itemData.MaxItemsInStack)
				{
					return i;
				}
			}

			return -1;
		}

#endregion
	}
}