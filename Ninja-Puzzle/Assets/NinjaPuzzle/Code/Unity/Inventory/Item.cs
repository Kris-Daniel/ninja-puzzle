namespace NinjaPuzzle.Code.Unity.Inventory
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