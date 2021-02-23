using System.Collections.Generic;
using System.Linq;
using NinjaPuzzle.Code.Unity.Inventory;

namespace NinjaPuzzle.Code.Gameplay.Inventory
{
	public class Inventory
	{
		private const int MaxStacks = 16;
		public List<ItemStack> Items = new List<ItemStack>();

		public bool SafeAdd(ItemData itemData, uint count)
		{
			bool canAdd = true;

			ItemStack foundedStack = null;
			
			var foundedStacks = Items.Where(stack => stack.ItemData == itemData).ToList();

			if (foundedStacks.Count > 0)
			{
				foundedStack = foundedStacks.Find(stack => stack.Count < stack.ItemData.MaxItemsInStack);
			}
			
			if (foundedStack != null)
			{
				int itemRemains = foundedStack.Add(count);
				if (itemRemains > 0)
				{
					Items.Add(new ItemStack(itemData, 0));
					return SafeAdd(itemData, (uint) itemRemains);
				}
			}
			else if (Items.Count < MaxStacks)
			{
				ItemStack newStack = new ItemStack(itemData, 0);
				int itemRemains = newStack.Add(count);
				Items.Add(newStack);
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
			
			ItemStack foundedStack = Items.Find(stack => stack.ItemData == itemData);
			if (foundedStack != null)
			{
				int itemRemains = foundedStack.Get(count);
				
				if (itemRemains > 0 || itemRemains < 0)
				{
					Items.Remove(foundedStack);
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
	}
}