using NinjaPuzzle.Code.UI.FrameWork;
using NinjaPuzzle.Code.UI.Uxml.Components.ItemCellComponent;
using NinjaPuzzle.Code.Unity.Systems.Inventory;
using UnityEngine.UIElements;

namespace NinjaPuzzle.Code.UI.Uxml.Components.HotBarComponent
{
	public class HotBarXml : AXmlController
	{
		private VisualElement m_hotbarBand;
		private Inventory m_playerInventory;
		
		public HotBarXml(AXmlController parent, VisualElement xmlElement) : base(parent, xmlElement)
		{
			m_hotbarBand = XmlElement.Q("inventory");
			EventManager.OnPlayerInventoryInit += RenderOnInit;
		}

		void RenderOnInit(Inventory inventory)
		{
			m_playerInventory = inventory;
			inventory.OnChange += Render;
			Render();
		}

		public override void Render()
		{
			m_hotbarBand.Clear();
			
			for (int i = 0; i < m_playerInventory.Stacks.Length && i < 5; i++)
			{
				var grid = ItemCellXml.InventoryGrid();
				var itemCellLanding = ItemCellXml.ItemCellLanding(i.ToString());
				
				grid.Add(itemCellLanding);

				if (m_playerInventory.Stacks[i].ItemData)
				{
					var itemCell = ItemCellXml.ItemCell(m_playerInventory.Stacks[i], i.ToString());
					itemCellLanding.Add(itemCell);
				}

				m_hotbarBand.Add(grid);
			}
		}
	}
}