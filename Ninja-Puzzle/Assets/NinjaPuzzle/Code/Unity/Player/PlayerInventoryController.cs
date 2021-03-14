using NinjaPuzzle.Code.Unity.Systems.Inventory;

namespace NinjaPuzzle.Code.Unity.Player
{
	public class PlayerInventoryController : InventoryController
	{
		protected override void Awake()
		{
			base.Awake();
			UnityGameInstance.InventoryManager.FillInventoryDefault(Inventory);
			UnityGameInstance.InventoryManager.ToggleInventoryReference(Inventory);
		}

		private void Start()
		{
			UnityGameInstance.EventManager.OnPlayerInventoryInit?.Invoke(Inventory);

			/*foreach (var inventoryStack in Inventory.Stacks)
			{
				print(inventoryStack.ItemData.ItemName + " = " + inventoryStack.Count);
			}*/
		}
	}
}