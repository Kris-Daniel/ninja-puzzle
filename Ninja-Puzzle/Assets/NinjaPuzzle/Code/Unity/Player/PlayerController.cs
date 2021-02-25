using NinjaPuzzle.Code.Unity.Managers;
using NinjaPuzzle.Code.Unity.Systems.Inventory;

namespace NinjaPuzzle.Code.Unity.Player
{
	public class PlayerController : AUnityMonoManager
	{
		public Inventory Inventory { get; private set; }

		protected override void Awake()
		{
			base.Awake();
			UnityGameInstance.InputManager.Events[EButtonEvent.OnFire1].Event += Fire1;
			UnityGameInstance.InputManager.Events[EButtonEvent.OnFire2].Event += Fire2;
			
			Inventory = UnityGameInstance.InventoryManager.CreateInventory(10);
			UnityGameInstance.InventoryManager.FillInventoryDefault(Inventory);
			
			foreach (var inventoryStack in Inventory.Stacks)
			{
				print(inventoryStack);
			}
		}

		private void Fire1(EEventStage eventStage)
		{
			if (eventStage == EEventStage.Click)
			{
				print("Katana Slash");
			}
		}
		
		private void Fire2(EEventStage eventStage)
		{
			if (eventStage == EEventStage.Click)
			{
				print("Kunai Rope");
			}
		}
	}
}