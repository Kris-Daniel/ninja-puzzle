using NinjaPuzzle.Code.Gameplay.Inventory;
using UnityEngine;

namespace NinjaPuzzle.Code.Unity.Inventory
{
	[CreateAssetMenu(fileName = "Item", menuName = "GamePlay/Item", order = 0)]
	public class ItemData : ScriptableObject
	{
		[SerializeField] private string itemName;
		[SerializeField] private EItem itemType;
		[SerializeField] private int maxItemsInStack = 1;
		[SerializeField] private ItemController prefab;

		public string ItemName => itemName;
		public int MaxItemsInStack => maxItemsInStack;
		public EItem ItemType => itemType;
		public ItemController Prefab => prefab;

		public Item GetItem()
		{
			return new Item(this, Instantiate(prefab));
		}
	}
}