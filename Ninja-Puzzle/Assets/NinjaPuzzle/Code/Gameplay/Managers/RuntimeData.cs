using NinjaPuzzle.Code.Gameplay.Interfaces;

namespace NinjaPuzzle.Code.Gameplay.Managers
{
	public class RuntimeData : IGameSaveData
	{
		public int LevelProgress { get; set; }

		public RuntimeData(IGameSaveData gameSaveData)
		{
			LevelProgress = gameSaveData.LevelProgress;
		}
	}
}