using NinjaPuzzle.Code.UI.Uxml;
using UnityEngine;
using UnityEngine.UIElements;

namespace NinjaPuzzle.Code.UI
{
	public class UiController : MonoBehaviour
	{
		private UIDocument m_uiDocument;
		public App App { get; private set; }

		private void Awake()
		{
			m_uiDocument = GetComponent<UIDocument>();
			App = new App(null, m_uiDocument.rootVisualElement.Q("app"));
			App.Render();
		}
	}
}