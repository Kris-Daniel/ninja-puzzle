using NinjaPuzzle.Code.UI.Uxml.Mixins;
using NinjaPuzzle.Code.Unity.Systems.Inventory;
using UnityEngine;
using UnityEngine.UIElements;

namespace NinjaPuzzle.Code.UI.Uxml.Components.ItemCellComponent
{
	public class ItemCellXml : AXmlController
	{
		private readonly TextElement m_name;
		private readonly TextElement m_counter;

		private static ItemCellXml currentItemCell;
		private static Vector2 startPos;
		
		public ItemStack ItemStack { get; private set; }
		public Inventory Inventory { get; private set; }

		
		public int Index = -1;
		
		public ItemCellXml(AXmlController parent, VisualElement xmlElement, ItemStack itemStack, Inventory inventory) : base(parent, xmlElement)
		{
			m_name = xmlElement.Q<TextElement>("item-cell_name");
			m_counter = xmlElement.Q<TextElement>("item-cell_count");
			ItemStack = itemStack;
			Inventory = inventory;
			m_name.text = itemStack.ItemData.name;
			m_counter.text = itemStack.Count.ToString();
		}
	}
}