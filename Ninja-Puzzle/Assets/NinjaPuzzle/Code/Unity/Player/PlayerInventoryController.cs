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
		}

		protected override void ToggleInventoryUI(EEventStage eventStage)
		{
			if (eventStage == EEventStage.Down)
			{
				UnityGameInstance.Game.GameController.RuntimeData.OnToggleInventory?.Invoke(Inventory);
			}
		}
	}
}