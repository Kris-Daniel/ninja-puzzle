using System.Linq;
using UnityEditor;
using UnityEngine.UIElements;

namespace NinjaPuzzle.Code.UI.FrameWork
{
	public static class PathManager
	{
		public static string UxmlPath = "Assets/NinjaPuzzle/Code/UI/Uxml/";

		public static VisualTreeAsset GetTemplate(string uxmlFile)
		{
			return AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(UxmlPath + uxmlFile + ".uxml");
		}

		public static VisualElement GetVisualElement(string uxmlFile)
		{
			return GetTemplate(uxmlFile).CloneTree().Children().ToList()[0];
		}
	}
}