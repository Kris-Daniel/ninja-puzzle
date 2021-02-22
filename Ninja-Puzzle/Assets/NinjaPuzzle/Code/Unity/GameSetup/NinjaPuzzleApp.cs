using NinjaPuzzle.Code.Gameplay;
using NinjaPuzzle.Code.Unity.Helpers.Singleton;

namespace NinjaPuzzle.Code.Unity.GameSetup
{
	public class NinjaPuzzleApp : ASingleton<NinjaPuzzleApp>
	{
		public UnityGameInstance UnityGameInstance { get; private set; }
		public GameInstance GameInstance { get; private set; }
		public GameController GameController => GameInstance.GameController;

		protected override void Init()
		{
			UnityGameInstance = UnityGameSetupFactory.Create();
			
			UnityGameInstance.CreateGameInstance();
			
			GameInstance = UnityGameInstance.Game;
			
			GameController.EventManager.OnGameInitialized.Invoke(GameController);
		}

		private void Update()
		{
			GameInstance?.Update();
			UnityGameInstance?.Update();
		}
	}
}