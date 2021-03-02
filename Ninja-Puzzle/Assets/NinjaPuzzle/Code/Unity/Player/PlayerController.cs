using NinjaPuzzle.Code.Unity.Enums;
using NinjaPuzzle.Code.Unity.Managers;
using NinjaPuzzle.Code.Unity.Systems.Inventory;

namespace NinjaPuzzle.Code.Unity.Player
{
	public class PlayerController : AMonoRefToUnityGameInstance
	{
		private Inventory m_inventory;
		
		protected override void Awake()
		{
			base.Awake();
			UnityGameInstance.InputManager.Events[EGameState.GamePlay][EButtonEvent.OnFire1].Event += Fire1;
			UnityGameInstance.InputManager.Events[EGameState.GamePlay][EButtonEvent.OnFire2].Event += Fire2;
		}

		private void Start()
		{
			m_inventory = GetComponent<PlayerInventoryController>().Inventory;
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
			if (eventStage == EEventStage.Hold)
			{
				print("Draw");
			}
			
			if (eventStage == EEventStage.Up)
			{
				m_inventory.SafeUseFromIndex(1, 1);
			}
		}
	}
}