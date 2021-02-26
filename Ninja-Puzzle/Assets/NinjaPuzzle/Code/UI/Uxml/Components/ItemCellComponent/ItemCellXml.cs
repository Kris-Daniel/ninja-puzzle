using NinjaPuzzle.Code.UI.Uxml.Mixins;
using NinjaPuzzle.Code.Unity.Systems.Inventory;
using UnityEngine;
using UnityEngine.UIElements;

namespace NinjaPuzzle.Code.UI.Uxml.Components.ItemCellComponent
{
	public class ItemCellXml : AXmlController
	{
		public ItemCellXml(AXmlController parent, VisualElement xmlElement) : base(parent, xmlElement)
		{}

		public static void Render(VisualElement xmlElement, ItemStack itemStack)
		{
			TextElement name = xmlElement.Q<TextElement>("item-cell_name");
			TextElement counter = xmlElement.Q<TextElement>("item-cell_count");

			name.text = itemStack.ItemData.ItemName;
			counter.text = itemStack.Count.ToString();
		}
	}
}