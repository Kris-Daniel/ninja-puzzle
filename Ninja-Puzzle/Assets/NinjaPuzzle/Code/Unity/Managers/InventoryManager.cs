using System.Collections.Generic;
using NinjaPuzzle.Code.Unity.GameSetup;
using NinjaPuzzle.Code.Unity.Systems.Inventory;

namespace NinjaPuzzle.Code.Unity.Managers
{
	public class InventoryManager : AUnityManager
	{
		private readonly ScriptableDataManager m_scriptableDataManager;
		
		public InventoryManager(UnityGameInstance unityGameInstance) : base(unityGameInstance)
		{
			m_scriptableDataManager = unityGameInstance.ScriptableDataManager;
		}

		public Inventory CreateInventory(int stacksCount)
		{
			return new Inventory(stacksCount);
		}

		public void FillInventoryDefault(Inventory inventory)
		{
			Dictionary<EItem, uint> defaultItems = new Dictionary<EItem, uint>
			{
				{EItem.Katana, 1},
				{EItem.Kunai, 1},
				{EItem.ShurikenAttract, 2},
				{EItem.ShurikenRepel, 2}
			};
			
			foreach (var keyValuePair in defaultItems)
			{
				inventory.SafeAdd(m_scriptableDataManager.Items[keyValuePair.Key], keyValuePair.Value);
			}
		}
	}
}