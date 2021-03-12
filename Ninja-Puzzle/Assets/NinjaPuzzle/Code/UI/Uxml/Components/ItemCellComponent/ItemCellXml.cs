using NinjaPuzzle.Code.UI.FrameWork;
using NinjaPuzzle.Code.Unity.Systems.Inventory;
using UnityEngine.UIElements;

namespace NinjaPuzzle.Code.UI.Uxml.Components.ItemCellComponent
{
	public class ItemCellXml : AXmlController
	{
		public ItemCellXml(AXmlController parent, VisualElement xmlElement) : base(parent, xmlElement) { }

		public static VisualElement InventoryGrid()
		{
			VisualElement grid = new VisualElement();
			grid.AddToClassList("inventory-grid");

			return grid;
		}
		
		public static VisualElement ItemCellLanding(string viewDataKey)
		{
			VisualElement itemCellLanding = new VisualElement();
			itemCellLanding.AddToClassList("item-cell-landing");
			itemCellLanding.name = "item-cell-landing";
			itemCellLanding.viewDataKey = viewDataKey;

			return  itemCellLanding;
		}

		public static VisualElement ItemCell(ItemStack itemStack, string viewDataKey)
		{
			VisualElement itemCell = PathManager.GetVisualElement("Components/ItemCellComponent/ItemCell");
			itemCell.viewDataKey = viewDataKey;

			FillItemCell(itemCell, itemStack);

			return itemCell;
		}

		public static void FillItemCell(VisualElement itemCell, ItemStack itemStack)
		{
			TextElement name = itemCell.Q<TextElement>("item-cell_name");
			TextElement counter = itemCell.Q<TextElement>("item-cell_count");

			name.text = itemStack.ItemData.ItemName;
			counter.text = itemStack.Count.ToString();
		}
	}
}