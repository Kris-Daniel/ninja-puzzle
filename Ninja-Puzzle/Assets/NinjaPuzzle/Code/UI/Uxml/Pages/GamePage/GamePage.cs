using NinjaPuzzle.Code.UI.FrameWork;
using NinjaPuzzle.Code.UI.Uxml.Layouts.GuiLayout;
using NinjaPuzzle.Code.UI.Uxml.Layouts.InventoryLayout;
using UnityEngine.UIElements;

namespace NinjaPuzzle.Code.UI.Uxml.Pages.GamePage
{
	public class GamePage : AXmlController
	{
		private GuiLayoutXml m_guiLayoutXml;
		private InventoryLayoutXml m_inventoryLayoutXml;
		
		public GamePage(AXmlController parent, VisualElement xmlElement) : base(parent, xmlElement)
		{
			m_guiLayoutXml = new GuiLayoutXml(this, xmlElement.Q("gui-layout"));
			m_inventoryLayoutXml = new InventoryLayoutXml(this, xmlElement.Q("inventory-layout"));
			
			m_inventoryLayoutXml.XmlElement.parent.AddToClassList("hide");
		}
	}
}