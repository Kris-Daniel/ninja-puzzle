using UnityEditor;
using UnityEngine.UIElements;

namespace NinjaPuzzle.Code.UI.Uxml.Mixins
{
	public static class PathStore
	{
		public static string UxmlPath = "Assets/NinjaPuzzle/Code/UI/Uxml/";

		public static VisualTreeAsset GetTemplate(string uxmlFile)
		{
			return AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(UxmlPath + uxmlFile + ".uxml");
		}
	}
}