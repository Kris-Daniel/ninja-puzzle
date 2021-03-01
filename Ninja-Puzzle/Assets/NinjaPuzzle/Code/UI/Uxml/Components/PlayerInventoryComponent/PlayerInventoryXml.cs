using System.Linq;
using NinjaPuzzle.Code.UI.Uxml.Components.ItemCellComponent;
using NinjaPuzzle.Code.UI.Uxml.Mixins;
using NinjaPuzzle.Code.Unity.Systems.Inventory;
using UnityEngine.UIElements;

namespace NinjaPuzzle.Code.UI.Uxml.Components.PlayerInventoryComponent
{
	public class PlayerInventoryXml : AXmlController
	{
		private readonly VisualElement m_inventoryTop;
		private readonly VisualElement m_inventoryBottom;
		private readonly VisualTreeAsset m_cellTemplate;
		private Inventory m_inventory;
		
		public PlayerInventoryXml(AXmlController parent, VisualElement xmlElement) : base(parent, xmlElement)
		{
			m_inventoryTop = XmlElement.Q("inventory_top");
			m_inventoryBottom = XmlElement.Q("inventory_bottom");
			m_cellTemplate = PathStore.GetTemplate("Components/ItemCellComponent/ItemCell");
		}

		public void Render(Inventory inventory)
		{
			m_inventory = inventory;
			XmlElement.viewDataKey = inventory.Key.ToString();
			inventory.OnChange += RenderItemCells;
			RenderItemCells();
		}

		public void UnRender(Inventory inventory)
		{
			inventory.OnChange -= RenderItemCells;
			XmlElement.viewDataKey = "";
			m_inventory = null;
		}

		void RenderItemCells()
		{
			m_inventoryTop.Clear();
			m_inventoryBottom.Clear();
			
			for (int i = 0; i < m_inventory.Stacks.Length; i++)
			{
				VisualElement grid = new VisualElement();
				grid.AddToClassList("inventory-grid");
				
				VisualElement itemCellLanding = new VisualElement();
				itemCellLanding.AddToClassList("item-cell-landing");
				itemCellLanding.name = "item-cell-landing";
				itemCellLanding.viewDataKey = i.ToString();
				
				grid.Add(itemCellLanding);

				if (m_inventory.Stacks[i].ItemData)
				{
					VisualElement itemCell = m_cellTemplate.CloneTree().Children().ToList()[0]; //template
					ItemCellXml.Render(itemCell, m_inventory.Stacks[i]);
					itemCell.viewDataKey = i.ToString();
					itemCellLanding.Add(itemCell);
				}

				VisualElement inventoryBox = i < 10 ? m_inventoryTop : m_inventoryBottom;
				inventoryBox.Add(grid);
			}
		}
	}
}