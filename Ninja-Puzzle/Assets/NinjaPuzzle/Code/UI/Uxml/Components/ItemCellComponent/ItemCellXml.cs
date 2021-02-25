using NinjaPuzzle.Code.UI.Uxml.Mixins;
using UnityEngine.UIElements;

namespace NinjaPuzzle.Code.UI.Uxml.Components.ItemCellComponent
{
	public class ItemCellXml : AXmlController
	{
		private readonly TextElement m_name;
		private readonly TextElement m_counter;
		
		public ItemCellXml(AXmlController parent, VisualElement xmlElement) : base(parent, xmlElement)
		{
			m_name = xmlElement.Q<TextElement>("item-cell_name");
			m_counter = xmlElement.Q<TextElement>("item-cell_count");
		}

		public void Render(string name, int count)
		{
			m_name.text = name;
			m_counter.text = count.ToString();
		}
	}
}