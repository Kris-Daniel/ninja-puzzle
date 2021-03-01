using NinjaPuzzle.Code.Unity.Interfaces;

namespace NinjaPuzzle.Code.Gameplay
{
	public sealed class GameInstance
	{
		public GameController GameController { get; private set; }

		public GameInstance(IGameSaveData gameSaveData)
		{
			GameController = new GameController(this, gameSaveData);
		}

		public void Update()
		{
			GameController.Update();
		}
	}
}