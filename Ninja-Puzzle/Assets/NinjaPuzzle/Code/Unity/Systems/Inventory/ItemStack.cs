﻿using NinjaPuzzle.Code.Unity.ScriptableObjects.Inventory;
using UnityEngine;

namespace NinjaPuzzle.Code.Unity.Systems.Inventory
{
	public struct ItemStack
	{
		
		public ItemData ItemData { get; private set; }
		public int Count { get; private set; }

		public ItemStack(ItemData itemData, uint count = 1)
		{
			ItemData = itemData;
			Count = Mathf.Clamp((int) count, 0, ItemData.MaxItemsInStack);
		}
 
		public int Add(uint count)
		{
			int itemsRemains = 0;
			Count += (int) count;
			
			if (Count > ItemData.MaxItemsInStack)
			{
				itemsRemains = Count - ItemData.MaxItemsInStack;
				Count = ItemData.MaxItemsInStack;
			}

			return itemsRemains;
		}

		public int Get(uint count)
		{
			Count -= (int) count;

			int itemsRemains = Count == 0 ? -1 : 0;

			if (Count < 0)
			{
				itemsRemains = Mathf.Abs(Count);
				Count = 0;
			}
			
			return itemsRemains;
		}

		public void ChangeCount(int difference)
		{
			Count = Mathf.Clamp(Count + difference, 0, ItemData.MaxItemsInStack);
		}

		public void Print()
		{
			Debug.Log(ItemData.ItemName + " = " + Count);
		}
	}
}