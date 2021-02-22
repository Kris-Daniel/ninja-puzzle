using NinjaPuzzle.Code.Unity.GameSetup;
using NinjaPuzzle.Code.Unity.Managers;
using UnityEngine;

namespace NinjaPuzzle.Code.Unity.Player
{
	public class PlayerController : MonoBehaviour
	{
		private InputManager m_inputManager;

		private void Start()
		{
			m_inputManager = NinjaPuzzleApp.Instance.UnityGameInstance.InputManager;
			m_inputManager.Events[EButtonEvent.OnFire1].Event += Fire1;
			m_inputManager.Events[EButtonEvent.OnFire2].Event += Fire2;
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