using NinjaPuzzle.Code.UI.FrameWork;
using NinjaPuzzle.Code.UI.Uxml.Components.ItemCellComponent;
using NinjaPuzzle.Code.Unity.Systems.Inventory;
using UnityEngine.UIElements;

namespace NinjaPuzzle.Code.UI.Uxml.Components.PlayerInventoryComponent
{
	public class PlayerInventoryXml : AXmlController
	{
		private readonly VisualElement m_inventoryTop;
		private Inventory m_inventory;
		
		public PlayerInventoryXml(AXmlController parent, VisualElement xmlElement) : base(parent, xmlElement)
		{
			m_inventoryTop = XmlElement.Q("inventory_top");
		}

		public void Show(Inventory inventory)
		{
			m_inventory = inventory;
			XmlElement.viewDataKey = inventory.Key.ToString();
			inventory.OnChange += RenderItemCells;
			RenderItemCells();
		}

		public void Hide(Inventory inventory)
		{
			inventory.OnChange -= RenderItemCells;
			XmlElement.viewDataKey = "";
			m_inventory = null;
		}

		void RenderItemCells()
		{
			m_inventoryTop.Clear();
			
			for (int i = 5; i < m_inventory.Stacks.Length; i++)
			{
				var grid = ItemCellXml.InventoryGrid();
				var itemCellLanding = ItemCellXml.ItemCellLanding(i.ToString());
				
				grid.Add(itemCellLanding);
				
				if (m_inventory.Stacks[i].ItemData)
				{
					var itemCell = ItemCellXml.ItemCell(m_inventory.Stacks[i], i.ToString());
					itemCellLanding.Add(itemCell);
				}

				m_inventoryTop.Add(grid);
			}
		}
	}
}