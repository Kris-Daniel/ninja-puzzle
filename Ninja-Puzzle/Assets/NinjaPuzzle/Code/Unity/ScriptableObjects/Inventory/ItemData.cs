using NinjaPuzzle.Code.Unity.Systems.Inventory;
using UnityEngine;

namespace NinjaPuzzle.Code.Unity.ScriptableObjects.Inventory
{
	[CreateAssetMenu(fileName = "Item", menuName = "GamePlay/Item", order = 0)]
	public class ItemData : ScriptableObject
	{
		[SerializeField] private string itemName;
		[SerializeField] private int maxItemsInStack = 1;
		[SerializeField] private ItemController prefab;
		[SerializeField] private int inventoryIndex;

		public string ItemName => itemName;
		public int MaxItemsInStack => maxItemsInStack;
		public int InventoryIndex => inventoryIndex;
		public ItemController Prefab => prefab;

		public Item GetItem()
		{
			return new Item(this, Instantiate(prefab));
		}
	}
}