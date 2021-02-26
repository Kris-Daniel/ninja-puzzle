using System.Collections.Generic;
using UnityEngine.UIElements;

namespace NinjaPuzzle.Code.UI.Uxml.Mixins
{
	public class PageXml : AXmlController
	{
		public Dictionary<VisualElement, AXmlController> XmlControllersByVisualElement = new Dictionary<VisualElement, AXmlController>();

		public PageXml(AXmlController parent, VisualElement xmlElement) : base(parent, xmlElement)
		{
			XmlControllersByVisualElement.Clear();
			// TODO: remove unsued from dictionary
		}
	}
}