using NinjaPuzzle.Code.UI.FrameWork;
using NinjaPuzzle.Code.UI.Uxml.Components.HotBarComponent;
using UnityEngine.UIElements;

namespace NinjaPuzzle.Code.UI.Uxml.Layouts.GuiLayout
{
	public class GuiLayoutXml : AXmlController
	{
		public HotBarXml HotBarXml { get; private set; }
		
		public GuiLayoutXml(AXmlController parent, VisualElement xmlElement) : base(parent, xmlElement)
		{
			HotBarXml = new HotBarXml(this, XmlElement.Q("hotbar"));
		}
	}
}