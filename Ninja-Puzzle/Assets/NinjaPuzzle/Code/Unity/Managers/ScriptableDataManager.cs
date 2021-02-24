using NinjaPuzzle.Code.Unity.GameSetup;
using NinjaPuzzle.Code.Unity.Helpers.GenericDictionary;
using NinjaPuzzle.Code.Unity.ScriptableObjects.Inventory;
using NinjaPuzzle.Code.Unity.Systems.Inventory;
using UnityEngine;

namespace NinjaPuzzle.Code.Unity.Managers
{
	public class ScriptableDataManager : AUnityManager
	{
		public GenericDictionary<EItem, ItemData> Items { get; private set; }
		
		public ScriptableDataManager(UnityGameInstance unityGameInstance) : base(unityGameInstance)
		{
			var globalData = Resources.Load<GlobalDataByEnum>("GlobalDataByEnum");
			Items = globalData.Items;
		}
	}
}