using UnityEngine.UIElements;

namespace NinjaPuzzle.Code.UI.Uxml.Mixins
{
	public static class Extensions
	{
		public static VisualElement ClosestParent(this VisualElement xmlElement, string parentName, int count = 0)
		{
			while (xmlElement != null && xmlElement.name != parentName && count < 10)
			{
				xmlElement = xmlElement.parent;
				count++;
			}

			if (xmlElement == null || count >= 10)
			{
				return null;
			}

			return xmlElement;
		}
	}
}