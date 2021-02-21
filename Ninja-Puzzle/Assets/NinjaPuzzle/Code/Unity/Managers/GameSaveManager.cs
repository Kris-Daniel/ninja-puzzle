using NinjaPuzzle.Code.Gameplay.Interfaces;

namespace NinjaPuzzle.Code.Unity.Managers
{
	public class GameSaveManager : IGameSaveData
	{
		public int LevelProgress { get; set; }
		public IGameSaveData CurrentGameSave => this;
		
		public GameSaveManager()
		{
			LevelProgress = 1;
		}
	}
}