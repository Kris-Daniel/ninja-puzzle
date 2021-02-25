using System.Collections.Generic;
using NinjaPuzzle.Code.UI.Uxml.Components.ItemCellComponent;
using NinjaPuzzle.Code.UI.Uxml.Mixins;
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
			xmlElement.AddToClassList("hide");
			
			//Init item cells
			var visualItemCells = xmlElement.Query<VisualElement>("item-cell").ToList();
			foreach (var visualItemCell in visualItemCells)
			{
				ItemCells.Add(new ItemCellXml(this, visualItemCell));
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