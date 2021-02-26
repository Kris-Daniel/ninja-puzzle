using System.Linq;
using NinjaPuzzle.Code.UI.Uxml.Components.ItemCellComponent;
using NinjaPuzzle.Code.UI.Uxml.Mixins;
using NinjaPuzzle.Code.Unity.Managers;
using NinjaPuzzle.Code.Unity.Systems.Inventory;
using UnityEngine;
using UnityEngine.UIElements;

namespace NinjaPuzzle.Code.UI.Uxml.Components.InventoryComponent
{
	public class InventoryXml : AXmlController
	{
		public Inventory Inventory { get; private set; }
		
		private VisualElement m_inventoryTop;
		private VisualTreeAsset m_cellTemplate;
		
		public InventoryXml(AXmlController parent, VisualElement xmlElement) : base(parent, xmlElement)
		{
			XmlElement.AddToClassList("hide");
			UnityGameInstance.InputManager.Events[EButtonEvent.OnInventory].Event += ToggleInventory;

			m_inventoryTop = XmlElement.Q("inventory_top");
			m_cellTemplate = PathStore.GetTemplate("Components/ItemCellComponent/ItemCell");
		}

		private void ToggleInventory(EEventStage eventStage)
		{
			if (eventStage == EEventStage.Down)
			{
				XmlElement.ToggleInClassList("hide");
			}
		}

		public void SetData(Inventory inventory)
		{
			Inventory = inventory;
			Inventory.OnChange += SetItemCellsData;
			SetItemCellsData();
		}

		void SetItemCellsData()
		{
			m_inventoryTop.Clear();
			for (int i = 0; i < Inventory.Stacks.Length; i++)
			{
				VisualElement grid = new VisualElement();
				grid.AddToClassList("inventory-grid");

				if (Inventory.Stacks[i].ItemData)
				{
					VisualElement itemCell = m_cellTemplate.CloneTree(); //template
					grid.Add(itemCell);
					var itemCellXml = new ItemCellXml(this, itemCell, Inventory.Stacks[i], Inventory);
					itemCellXml.XmlElement.viewDataKey = i.ToString();
				}
				
				m_inventoryTop.Add(grid);
			}
		}
	}
}