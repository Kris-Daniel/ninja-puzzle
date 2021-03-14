using NinjaPuzzle.Code.Unity.Camera;
using NinjaPuzzle.Code.Unity.Enums;
using NinjaPuzzle.Code.Unity.Managers;
using NinjaPuzzle.Code.Unity.Systems.Inventory;
using NinjaPuzzle.Code.Unity.Tools;
using UnityEngine;

namespace NinjaPuzzle.Code.Unity.Player
{
	public class PlayerController : AMonoRefToUnityGameInstance
	{
		[SerializeField] private ProjectileLauncher projectileLauncher;
		
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
			if (eventStage == EEventStage.Down)
			{
				if (m_inventory.Stacks[2].Count > 0)
				{
					var itemGo = Instantiate(m_inventory.Stacks[2].ItemData.Prefab);
					projectileLauncher.LoadProjectile(itemGo.Rigidbody);
				}
			}
			
			if (eventStage == EEventStage.Hold)
			{
				// spawn sphere
			}
			
			if (eventStage == EEventStage.Up)
			{
				projectileLauncher.Launch(MainCamera.Instance.RayCastForward());
				m_inventory.SafeUseFromIndex(2, 1);
			}
		}
	}
}