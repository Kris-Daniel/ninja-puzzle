using NinjaPuzzle.Code.UI.Uxml.Components.InventoryComponent;
using NinjaPuzzle.Code.UI.Uxml.Mixins;
using NinjaPuzzle.Code.Unity.Systems.Inventory;
using UnityEngine.UIElements;

namespace NinjaPuzzle.Code.UI.Uxml.Pages.GuiPage
{
	public class GuiPageXml : PageXml
	{
		public GuiPageManager GuiPageManager { get; private set; }
		public InventoryXml InventoryXml { get; private set; }
		private XmlDragController m_xmlDragController;
		
		public GuiPageXml(AXmlController parent, VisualElement xmlElement, GuiPageManager guiPageManager) : base(parent, xmlElement)
		{
			GuiPageManager = guiPageManager;
			
			m_xmlDragController = new XmlDragController(this, xmlElement);
			
			InventoryXml = new InventoryXml(this, xmlElement.Q("inventory"));
		}

		public void ToggleInventoryUI(Inventory inventory)
		{
			InventoryXml.XmlElement.ToggleInClassList("hide");
			UnityGameInstance.InventoryManager.ToggleInventoryReference(inventory);
			
			if (!InventoryXml.XmlElement.ClassListContains("hide"))
			{
				InventoryXml.Render(inventory);
			}
			else
			{
				InventoryXml.UnRender(inventory);
			}
		}
	}
}