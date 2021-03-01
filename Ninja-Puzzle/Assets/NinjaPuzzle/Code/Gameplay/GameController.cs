using NinjaPuzzle.Code.Gameplay.Managers;
using NinjaPuzzle.Code.Unity.Interfaces;
using NinjaPuzzle.Code.Unity.Managers;

namespace NinjaPuzzle.Code.Gameplay
{
	public class GameController
	{
		public GameInstance GameInstance { get; private set; }
		public RuntimeData RuntimeData { get; private set; }
		
		public TimeManager TimeManager { get; private set; }
		public PlayerManager PlayerManager { get; private set; }

		public GameController(GameInstance gameInstance, IGameSaveData gameSaveData)
		{
			GameInstance = gameInstance;
			RuntimeData = new RuntimeData(gameSaveData);
			
			TimeManager = new TimeManager(this);
			PlayerManager = new PlayerManager(this);
		}

		public void Update()
		{
			PlayerManager.Update();
			TimeManager.Update();
		}
	}
}