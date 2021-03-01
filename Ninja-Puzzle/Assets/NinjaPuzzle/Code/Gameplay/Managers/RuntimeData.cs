using System;
using NinjaPuzzle.Code.Gameplay.Interfaces;
using NinjaPuzzle.Code.Unity.Systems.Inventory;

namespace NinjaPuzzle.Code.Gameplay.Managers
{
	public class RuntimeData : IGameSaveData
	{
		public int LevelProgress { get; set; }

		public Action<Inventory> OnToggleInventory;
		
		public RuntimeData(IGameSaveData gameSaveData)
		{
			LevelProgress = gameSaveData.LevelProgress;
		}
	}
}