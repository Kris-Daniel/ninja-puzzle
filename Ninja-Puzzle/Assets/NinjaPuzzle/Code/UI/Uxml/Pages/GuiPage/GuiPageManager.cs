using NinjaPuzzle.Code.Unity.Managers;
using UnityEngine.UIElements;

namespace NinjaPuzzle.Code.UI.Uxml.Pages.GuiPage
{
	public class GuiPageManager : AUnityMonoManager
	{
		private UIDocument m_uiDocument;
		private GuiPageXml m_guiPageXml;

		protected override void Awake()
		{
			base.Awake();
			m_uiDocument = GetComponent<UIDocument>();
		}

		private void Start()
		{
			m_guiPageXml = new GuiPageXml(null, m_uiDocument.rootVisualElement.Q("gui-page"), this);
		}
	}
}