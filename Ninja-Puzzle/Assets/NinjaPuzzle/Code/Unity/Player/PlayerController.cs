using NinjaPuzzle.Code.Unity.Enums;
using NinjaPuzzle.Code.Unity.Managers;

namespace NinjaPuzzle.Code.Unity.Player
{
	public class PlayerController : AMonoRefToUnityGameInstance
	{
		protected override void Awake()
		{
			base.Awake();
			UnityGameInstance.InputManager.Events[EGameState.GamePlay][EButtonEvent.OnFire1].Event += Fire1;
			UnityGameInstance.InputManager.Events[EGameState.GamePlay][EButtonEvent.OnFire2].Event += Fire2;
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