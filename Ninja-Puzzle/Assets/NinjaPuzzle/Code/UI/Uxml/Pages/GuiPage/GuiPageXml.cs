using System.Collections.Generic;
using NinjaPuzzle.Code.UI.Uxml.Layouts.GuiLayout;
using NinjaPuzzle.Code.UI.Uxml.Layouts.InventoryLayout;
using NinjaPuzzle.Code.UI.Uxml.Mixins;
using UnityEngine.UIElements;

namespace NinjaPuzzle.Code.UI.Uxml.Pages.GuiPage
{
	public class GuiPageXml : PageXml
	{
		public GuiPageManager GuiPageManager { get; private set; }
		private XmlDragController m_xmlDragController;
		
		private readonly List<VisualElement> m_layouts;

		// Child XmlControllers
		public InventoryLayoutXml InventoryLayout { get; private set; }
		public GuiLayoutXml GuiLayout { get; private set; }
		
		public GuiPageXml(AXmlController parent, VisualElement xmlElement, GuiPageManager guiPageManager) : base(parent, xmlElement)
		{
			GuiPageManager = guiPageManager;
			
			m_xmlDragController = new XmlDragController(this, xmlElement);
			
			m_layouts = xmlElement.Query<VisualElement>(null, "layout").ToList();
			
			// Child XmlControllers
			InventoryLayout = new InventoryLayoutXml(this, xmlElement.Q("inventory-layout"));
			GuiLayout = new GuiLayoutXml(this, xmlElement.Q("gui-layout"));
		}

		public void HideAllLayouts()
		{
			foreach (var visualElement in m_layouts)
			{
				visualElement.AddToClassList("hide");
			}
		}
	}
}