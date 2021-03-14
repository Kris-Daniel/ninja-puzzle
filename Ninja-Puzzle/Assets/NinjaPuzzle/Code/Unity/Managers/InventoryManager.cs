using System.Collections.Generic;
using NinjaPuzzle.Code.Unity.GameSetup;
using NinjaPuzzle.Code.Unity.Systems.Inventory;

namespace NinjaPuzzle.Code.Unity.Managers
{
	public class InventoryManager : AUnityManager
	{
		private readonly ScriptableDataManager m_scriptableDataManager;
		
		public Dictionary<int, Inventory> Inventories { get; private set; } = new Dictionary<int, Inventory>();
		
		public InventoryManager(UnityGameInstance unityGameInstance) : base(unityGameInstance)
		{
			m_scriptableDataManager = unityGameInstance.ScriptableDataManager;
		}

		public Inventory CreateInventory(int stacksCount)
		{
			return new Inventory(stacksCount);
		}

		public void ToggleInventoryReference(Inventory inventory)
		{
			if (Inventories.ContainsKey(inventory.Key))
			{
				Inventories.Remove(inventory.Key);
			}
			else
			{
				Inventories.Add(inventory.Key, inventory);
			}
		}

		public void FillInventoryDefault(Inventory inventory)
		{
			Dictionary<EItem, uint> defaultItems = new Dictionary<EItem, uint>
			{
				{EItem.Katana, 1},
				{EItem.Kunai, 1},
				{EItem.ShurikenAttract, 16},
				{EItem.ShurikenRepel, 32}
			};
			
			foreach (var keyValuePair in defaultItems)
			{
				var itemStack = new ItemStack(m_scriptableDataManager.Items[keyValuePair.Key], keyValuePair.Value);
				inventory.SafeAddToIndex(itemStack, itemStack.ItemData.InventoryIndex);
			}
		}
	}
}