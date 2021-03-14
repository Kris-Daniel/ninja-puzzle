using NinjaPuzzle.Code.Unity.Managers;
using UnityEngine;

namespace NinjaPuzzle.Code.Unity.Systems.Inventory
{
	public class InventoryController : AMonoRefToUnityGameInstance
	{
		[SerializeField] private int stacks = 4;
		public Inventory Inventory { get; private set; }

		protected override void Awake()
		{
			base.Awake();
			Inventory = UnityGameInstance.InventoryManager.CreateInventory(stacks);
		}
	}
}