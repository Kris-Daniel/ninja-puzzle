using NinjaPuzzle.Code.Unity.GameSetup;
using NinjaPuzzle.Code.Unity.Managers;
using NinjaPuzzle.Code.Unity.Systems.Inventory;
using UnityEngine;

namespace NinjaPuzzle.Code.Unity.Player
{
	public class PlayerController : MonoBehaviour
	{
		private UnityGameInstance m_unityGameInstance;
		public Inventory Inventory { get; private set; }

		private void Awake()
		{
			m_unityGameInstance = NinjaPuzzleApp.Instance.UnityGameInstance;
			m_unityGameInstance.InputManager.Events[EButtonEvent.OnFire1].Event += Fire1;
			m_unityGameInstance.InputManager.Events[EButtonEvent.OnFire2].Event += Fire2;
			
			Inventory = m_unityGameInstance.InventoryManager.CreateInventory(16);
			m_unityGameInstance.InventoryManager.FillInventoryDefault(Inventory);
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