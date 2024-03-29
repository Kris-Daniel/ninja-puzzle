﻿using NinjaPuzzle.Code.Unity.GameSetup;
using NinjaPuzzle.Code.Unity.Interfaces;

namespace NinjaPuzzle.Code.Unity.Managers
{
	public class GameSaveManager : AUnityManager, IGameSaveData
	{
		public int LevelProgress { get; set; }
		public IGameSaveData CurrentGameSave => this;
		
		public GameSaveManager(UnityGameInstance unityGameInstance) : base(unityGameInstance)
		{
			LevelProgress = 1;
		}
	}
}