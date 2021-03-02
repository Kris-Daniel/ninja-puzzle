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
				if (m_inventory.Stacks[1].Count > 0)
				{
					var itemGo = Instantiate(m_inventory.Stacks[1].ItemData.Prefab);
					projectileLauncher.SetProjectile(itemGo.Rigidbody);
				}
			}
			
			if (eventStage == EEventStage.Hold)
			{
				projectileLauncher.SetTarget(RayCastForward());
			}
			
			if (eventStage == EEventStage.Up)
			{
				projectileLauncher.Launch();
				projectileLauncher.ToggleDraw(false);
				m_inventory.SafeUseFromIndex(1, 1);
			}
		}

		Vector3 RayCastForward()
		{
			Vector3 result = Vector3.zero;

			Ray RayOrigin;
			RaycastHit HitInfo;
			
			RayOrigin = new Ray(MainCamera.Instance.Camera.transform.position, MainCamera.Instance.Camera.transform.forward);
			
			if (Physics.Raycast(RayOrigin, out HitInfo,20f))
			{
				Debug.DrawRay(RayOrigin.origin,HitInfo.point - RayOrigin.origin ,Color.yellow);
				
				result = HitInfo.point;
			}
			else
			{
				result = RayOrigin.origin + RayOrigin.direction * 20;
			}
			
			return result;
		}
	}
}