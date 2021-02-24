using NinjaPuzzle.Code.Unity.ScriptableObjects.Inventory;

namespace NinjaPuzzle.Code.Unity.Systems.Inventory
{
	public class Item
	{
		public ItemData ItemData { get; private set; }
		public ItemController ItemController { get; private set; }

		public Item(ItemData itemData, ItemController itemController)
		{
			ItemData = itemData;
			ItemController = itemController;
			ItemController.SetItem(this);
			ItemController.gameObject.SetActive(false);
		}
	}
}