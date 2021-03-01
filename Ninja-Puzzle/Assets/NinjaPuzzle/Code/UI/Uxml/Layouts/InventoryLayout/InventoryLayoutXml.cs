using NinjaPuzzle.Code.UI.Uxml.Components.PlayerInventoryComponent;
using NinjaPuzzle.Code.UI.Uxml.Mixins;
using NinjaPuzzle.Code.Unity.Systems.Inventory;
using UnityEngine.UIElements;

namespace NinjaPuzzle.Code.UI.Uxml.Layouts.InventoryLayout
{
	public class InventoryLayoutXml : AXmlController
	{
		// Child XmlControllers
		private readonly PlayerInventoryXml m_playerInventoryXml;
		
		public InventoryLayoutXml(AXmlController parent, VisualElement xmlElement) : base(parent, xmlElement)
		{
			m_playerInventoryXml = new PlayerInventoryXml(this, XmlElement.Q("inventory"));
			EventManager.OnToggleInventory += Toggle;
		}

		public void Toggle(Inventory inventory)
		{
			XmlElement.ToggleInClassList("hide");
			
			UnityGameInstance.InventoryManager.ToggleInventoryReference(inventory);
			
			if (!XmlElement.ClassListContains("hide"))
			{
				m_playerInventoryXml.Render(inventory);
			}
			else
			{
				m_playerInventoryXml.UnRender(inventory);
			}
		}
	}
}