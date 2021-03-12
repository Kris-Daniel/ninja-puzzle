using System.Collections.Generic;
using NinjaPuzzle.Code.Unity.GameSetup;
using NinjaPuzzle.Code.Unity.Managers;
using UnityEngine.UIElements;

namespace NinjaPuzzle.Code.UI.FrameWork
{
	public class AXmlController
	{
		public AXmlController Parent { get; private set; }
		public VisualElement XmlElement { get; set; }
		public List<AXmlController> Children { get; private set; } = new List<AXmlController>();
		
		public UnityGameInstance UnityGameInstance { get; private set; }
		public EventManager EventManager { get; private set; }
		
		public AXmlController(AXmlController parent, VisualElement xmlElement)
		{
			Parent = parent;
			XmlElement = xmlElement;
			
			UnityGameInstance = NinjaPuzzleApp.Instance.UnityGameInstance;
			EventManager = UnityGameInstance.EventManager;
		}
		
		public virtual void Render()
		{
			
		}
		
		public virtual void UpdateRender()
		{
		}
	}
}