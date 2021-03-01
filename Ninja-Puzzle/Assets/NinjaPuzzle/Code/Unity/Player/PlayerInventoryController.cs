using NinjaPuzzle.Code.Unity.Enums;
using NinjaPuzzle.Code.Unity.Managers;
using NinjaPuzzle.Code.Unity.Systems.Inventory;

namespace NinjaPuzzle.Code.Unity.Player
{
	public class PlayerInventoryController : InventoryController
	{
		protected override void Awake()
		{
			base.Awake();
			UnityGameInstance.InputManager.Events[EGameState.GamePlay][EButtonEvent.OnInventory].Event += ToggleInventoryUI;
			UnityGameInstance.InputManager.Events[EGameState.GamePlayInventory][EButtonEvent.OnInventory].Event += ToggleInventoryUI;
			UnityGameInstance.InventoryManager.FillInventoryDefault(Inventory);
			UnityGameInstance.InventoryManager.ToggleInventoryReference(Inventory);
		}

		private void Start()
		{
			UnityGameInstance.EventManager.OnPlayerInventoryInit?.Invoke(Inventory);
		}

		protected override void ToggleInventoryUI(EEventStage eventStage)
		{
			if (eventStage == EEventStage.Down)
			{
				UnityGameInstance.EventManager.OnToggleInventory?.Invoke(Inventory);
			}
		}
	}
}