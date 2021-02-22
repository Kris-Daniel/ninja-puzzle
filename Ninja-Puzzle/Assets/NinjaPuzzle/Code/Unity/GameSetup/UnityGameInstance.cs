using NinjaPuzzle.Code.Gameplay;
using NinjaPuzzle.Code.Unity.Managers;

namespace NinjaPuzzle.Code.Unity.GameSetup
{
	public sealed class UnityGameInstance
	{
		public GameSaveManager GameSaveManager { get; private set; }
		public GameInstance Game { get; private set; }
		public VfxManager VfxManager { get; private set; }
		public InputManager InputManager { get; private set; }

		public UnityGameInstance()
		{
			GameSaveManager = new GameSaveManager(this);
			VfxManager = new VfxManager(this);
			InputManager = new InputManager(this);
		}

		public void CreateGameInstance()
		{
			Game = new GameInstance(GameSaveManager.CurrentGameSave);
		}

		public void Update()
		{
			InputManager.Update();
			VfxManager.Update();
			GameSaveManager.Update();
		}
	}
}