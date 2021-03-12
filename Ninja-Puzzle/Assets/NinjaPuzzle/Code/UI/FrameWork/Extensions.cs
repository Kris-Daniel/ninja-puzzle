using System.Linq;
using UnityEngine.UIElements;

namespace NinjaPuzzle.Code.UI.FrameWork
{
	public static class Extensions
	{
		public static VisualElement ClosestParent(this VisualElement xmlElement, string parentName)
		{
			while (xmlElement.parent != null && xmlElement.name != parentName)
			{
				xmlElement = xmlElement.parent;
			}

			if (xmlElement.parent == null)
			{
				return null;
			}

			return xmlElement;
		}
		
		public static VisualElement GetRootElement(this VisualElement xmlElement)
		{
			while (xmlElement.parent != null)
			{
				xmlElement = xmlElement.parent;
			}
			
			return xmlElement.Children().ToList()[0];
		}
		
		public static AXmlController GetRootController(this AXmlController xmlController)
		{
			while (xmlController.Parent != null)
			{
				xmlController = xmlController.Parent;
			}
			
			return xmlController;
		}
	}
}