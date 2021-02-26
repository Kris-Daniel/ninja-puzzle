using NinjaPuzzle.Code.UI.Uxml.Pages.GuiPage;
using NinjaPuzzle.Code.Unity.Managers;
using NinjaPuzzle.Code.Unity.Systems.Inventory;

namespace NinjaPuzzle.Code.Unity.Player
{
	public class PlayerInventoryController : InventoryController
	{
		protected override void Awake()
		{
			base.Awake();
			UnityGameInstance.InputManager.Events[EButtonEvent.OnInventory].Event += ToggleInventoryUI;
			UnityGameInstance.InventoryManager.FillInventoryDefault(Inventory);
			
			foreach (var inventoryStack in Inventory.Stacks)
			{
				print(inventoryStack);
			}
		}

		protected override void ToggleInventoryUI(EEventStage eventStage)
		{
			if (eventStage == EEventStage.Down)
			{
				UnityGameInstance.GetUnityMonoManager<GuiPageManager>().GuiPageXml.ToggleInventoryUI(Inventory);
			}
		}
	}
}