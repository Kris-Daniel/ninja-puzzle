using UnityEngine.UIElements;

namespace NinjaPuzzle.Code.UI.Uxml.Mixins
{
	public class PageXml : AXmlController
	{
		public PageXml(AXmlController parent, VisualElement xmlElement) : base(parent, xmlElement)
		{
			UnityGameInstance.PageXml = this;
		}
	}
}