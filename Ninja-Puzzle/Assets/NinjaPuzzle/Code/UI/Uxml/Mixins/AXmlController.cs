using NinjaPuzzle.Code.Gameplay.Managers;
using NinjaPuzzle.Code.Unity.GameSetup;
using UnityEngine.UIElements;

namespace NinjaPuzzle.Code.UI.Uxml.Mixins
{
	public abstract class AXmlController
	{
		public AXmlController Parent { get; private set; }
		public VisualElement XmlElement { get; private set; }
		public UnityGameInstance UnityGameInstance { get; private set; }
		public RuntimeData RuntimeData { get; private set; }
		public EventManager EventManager { get; private set; }
		
		protected AXmlController(AXmlController parent, VisualElement xmlElement)
		{
			Parent = parent;
			XmlElement = xmlElement;
			UnityGameInstance = NinjaPuzzleApp.Instance.UnityGameInstance;
			RuntimeData = UnityGameInstance.Game.GameController.RuntimeData;
			EventManager = UnityGameInstance.Game.GameController.EventManager;
		}
	}
}