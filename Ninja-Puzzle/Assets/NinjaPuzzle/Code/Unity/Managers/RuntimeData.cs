using NinjaPuzzle.Code.Unity.Interfaces;

namespace NinjaPuzzle.Code.Unity.Managers
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