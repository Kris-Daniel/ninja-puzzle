using System.Collections.Generic;
using System.Linq;
using NinjaPuzzle.Code.Gameplay.Managers;
using NinjaPuzzle.Code.Unity.Inventory;
using UnityEngine;

namespace NinjaPuzzle.Code.Gameplay.Inventory
{
	public class InventoryManager : AGameManager
	{
		public Inventory Inventory { get; private set; }
		public List<ItemData> ScriptableItems;
		
		public InventoryManager(GameController gameController) : base(gameController)
		{
			Inventory = new Inventory();
			ScriptableItems = Resources.LoadAll<ItemData>("Inventory").ToList();
			DefaultInventory();
			
			foreach (var inventoryStack in Inventory.Items)
			{
				if (inventoryStack != null)
				{
					Debug.Log(inventoryStack.ItemData.ItemType + " = " + inventoryStack.Count);
				}
				else
				{
					Debug.Log(null);
				}
			}
		}

		void DefaultInventory()
		{
			Dictionary<EItem, uint> defaultItems = new Dictionary<EItem, uint>
			{
				{EItem.Katana, 1},
				{EItem.Kunai, 1},
				{EItem.ShurikenAttract, 5},
				{EItem.ShurikenRepel, 2}
			};
			
			foreach (var keyValuePair in defaultItems)
			{
				Inventory.SafeAdd(ScriptableItems.Find(scriptableItem => scriptableItem.ItemType == keyValuePair.Key), keyValuePair.Value);
			}
		}
	}
}