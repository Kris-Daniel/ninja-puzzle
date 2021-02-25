using NinjaPuzzle.Code.UI.Uxml.Components.InventoryComponent;
using NinjaPuzzle.Code.UI.Uxml.Mixins;
using NinjaPuzzle.Code.Unity.Player;
using UnityEngine.UIElements;

namespace NinjaPuzzle.Code.UI.Uxml.Pages.GuiPage
{
	public class GuiPageXml : AXmlController
	{
		public GuiPageManager GuiPageManager { get; private set; }
		public InventoryXml InventoryXml { get; private set; }
		
		public GuiPageXml(AXmlController parent, VisualElement xmlElement, GuiPageManager guiPageManager) : base(parent, xmlElement)
		{
			GuiPageManager = guiPageManager;
			
			InventoryXml = new InventoryXml(this, xmlElement.Q("inventory"));
			InventoryXml.SetData(UnityGameInstance.GetUnityMonoManager<PlayerController>().Inventory);
		}
	}
}