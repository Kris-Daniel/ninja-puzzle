using UnityEngine;
using UnityEngine.UIElements;

namespace NinjaPuzzle.Code.UI.Uxml.Pages.GuiPage
{
	public class GuiPageManager : MonoBehaviour
	{
		private UIDocument m_uiDocument;
		public GuiPageXml GuiPageXml { get; private set; }

		void Awake()
		{
			m_uiDocument = GetComponent<UIDocument>();
			GuiPageXml = new GuiPageXml(null, m_uiDocument.rootVisualElement.Q("gui-page"), this);
		}
	}
}