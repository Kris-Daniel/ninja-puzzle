using System.Collections.Generic;
using System.Linq;
using NinjaPuzzle.Code.Unity.Inventory;

namespace NinjaPuzzle.Code.Gameplay.Inventory
{
	public class Inventory
	{
		private const int MaxStacks = 16;
		//public List<ItemStack> Items = new List<ItemStack>();
		public ItemStack[] Items = new ItemStack[MaxStacks];

		public ItemStack SafeAddToIndex(ItemStack itemStack, int stackIndex)
		{
			if (Items[stackIndex].ItemData == itemStack.ItemData)
			{
				int itemRemains = Items[stackIndex].Add((uint)itemStack.Count);
				return new ItemStack(itemStack.ItemData, (uint) itemRemains);
			}
			return null;
		}
		
		public ItemStack SafeUseFromIndex(int stackIndex)
		{
			ItemStack itemStack = Items[stackIndex];
			Items[stackIndex] = null;
			return itemStack;
		}

		public bool SafeAdd(ItemData itemData, uint count)
		{
			bool canAdd = true;

			ItemStack foundedStack = null;
			
			int stackIndex = FindNotFilledStack(itemData);

			if (stackIndex > -1)
			{
				foundedStack = Items[stackIndex];
			}

			if (foundedStack != null)
			{
				int itemRemains = foundedStack.Add(count);
				if (itemRemains > 0)
				{
					int freeIndex = GetFreeIndex();
					if (GetStacksCount() < MaxStacks && freeIndex > -1)
					{
						Items[freeIndex] = new ItemStack(itemData, 0);
					}
					return SafeAdd(itemData, (uint) itemRemains);
				}
			}
			else if (GetStacksCount() < MaxStacks)
			{
				ItemStack newStack = new ItemStack(itemData, 0);
				int itemRemains = newStack.Add(count);

				int freeIndex = GetFreeIndex();
				if (freeIndex > -1)
				{
					Items[freeIndex] = newStack;
				}
				
				if (itemRemains > 0)
				{
					return SafeAdd(itemData, (uint) itemRemains);
				}
			}
			else
			{
				canAdd = false;
			}

			return canAdd;
		}

		public List<ItemData> SafeUse(ItemData itemData, uint count)
		{
			List<ItemData> itemsToUse = new List<ItemData>();
			
			int stackIndex = FindStackIndex(itemData);
			
			if (stackIndex > -1)
			{
				ItemStack foundedStack = Items[stackIndex];
				int itemRemains = foundedStack.Get(count);
				
				if (itemRemains > 0 || itemRemains < 0)
				{
					Items[stackIndex] = null;
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

		private int GetStacksCount()
		{
			return Items.Count(t => t != null);
		}

		private int GetFreeIndex()
		{
			for (int i = 0; i < Items.Length; i++)
			{
				if (Items[i] == null)
				{
					return i;
				}
			}

			return -1;
		}

		private int FindStackIndex(ItemData itemData)
		{
			for (var i = 0; i < Items.Length; i++)
			{
				if (Items[i] != null && Items[i].ItemData == itemData)
				{
					return i;
				}
			}

			return -1;
		}

		private int FindNotFilledStack(ItemData itemData)
		{
			for (int i = 0; i < Items.Length; i++)
			{
				if (Items[i] != null && Items[i].ItemData == itemData && Items[i].Count < itemData.MaxItemsInStack)
				{
					return i;
				}
			}

			return -1;
		}
	}
}