using System.Collections.Generic;
using NinjaPuzzle.Code.UI.Uxml.Components.ItemCellComponent;
using NinjaPuzzle.Code.UI.Uxml.Mixins;
using NinjaPuzzle.Code.Unity.Managers;
using NinjaPuzzle.Code.Unity.Systems.Inventory;
using UnityEngine.UIElements;

namespace NinjaPuzzle.Code.UI.Uxml.Components.InventoryComponent
{
	public class InventoryXml : AXmlController
	{
		public Inventory Inventory { get; private set; }
		
		public List<ItemCellXml> ItemCells { get; private set; } = new List<ItemCellXml>();
		
		public InventoryXml(AXmlController parent, VisualElement xmlElement) : base(parent, xmlElement)
		{
			XmlElement.AddToClassList("hide");
			UnityGameInstance.InputManager.Events[EButtonEvent.OnInventory].Event += ToggleInventory;
			
			//Init item cells
			var visualItemCells = XmlElement.Query<VisualElement>("item-cell").ToList();
			foreach (var visualItemCell in visualItemCells)
			{
				ItemCells.Add(new ItemCellXml(this, visualItemCell));
			}
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
			
			for (int i = 0; i < Inventory.Stacks.Length; i++)
			{
				if (Inventory.Stacks[i] != null)
				{
					ItemCells[i].Render(Inventory.Stacks[i].ItemData.ItemName, Inventory.Stacks[i].Count);
				}
			}
		}
	}
}