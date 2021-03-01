using System.Linq;
using NinjaPuzzle.Code.UI.Uxml.Components.ItemCellComponent;
using NinjaPuzzle.Code.UI.Uxml.Mixins;
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
			m_hotbarBand = XmlElement.Q("hotbar_band");
			EventManager.OnPlayerInventoryInit += RenderOnInit;
		}

		void RenderOnInit(Inventory inventory)
		{
			m_playerInventory = inventory;
			inventory.OnChange += Render;
			Render();
		}

		private void Render()
		{
			m_hotbarBand.Clear();
			
			for (int i = 0; i < m_playerInventory.Stacks.Length && i < 5; i++)
			{
				VisualElement grid = new VisualElement();
				grid.AddToClassList("inventory-grid");
				
				VisualElement itemCellLanding = new VisualElement();
				itemCellLanding.AddToClassList("item-cell-landing");
				itemCellLanding.name = "item-cell-landing";
				itemCellLanding.viewDataKey = i.ToString();
				
				grid.Add(itemCellLanding);

				if (m_playerInventory.Stacks[i].ItemData)
				{
					VisualElement itemCell = PathStore.ItemCell.CloneTree().Children().ToList()[0]; //template
					ItemCellXml.Render(itemCell, m_playerInventory.Stacks[i]);
					itemCell.viewDataKey = i.ToString();
					itemCellLanding.Add(itemCell);
				}

				m_hotbarBand.Add(grid);
			}
		}
	}
}