using System.Collections.Generic;
using NinjaPuzzle.Code.UI.FrameWork;
using NinjaPuzzle.Code.UI.Uxml.Pages.GamePage;
using NinjaPuzzle.Code.UI.Uxml.Pages.MenuPage;
using UnityEngine.UIElements;

namespace NinjaPuzzle.Code.UI.Uxml
{
	public class App : AXmlController
	{
		private readonly AXmlController m_dragController;
		private readonly AXmlController m_menuPage;
		private readonly AXmlController m_gamePage;
		
		public App(AXmlController parent, VisualElement xmlElement) : base(parent, xmlElement)
		{
			m_dragController = new XmlDragController(this, xmlElement);
			m_menuPage = new MenuPage(this, PathManager.GetVisualElement("Pages/MenuPage/MenuPage"));
			m_gamePage = new GamePage(this, PathManager.GetVisualElement("Pages/GamePage/GamePage"));
			
			Children.AddRange(new List<AXmlController>() {m_menuPage, m_gamePage});
		}
		
		public override void Render()
		{
			base.Render();
			
			XmlElement.Add(m_menuPage.XmlElement);
			XmlElement.Add(m_gamePage.XmlElement);
			
			m_menuPage.XmlElement.AddToClassList("hide");
		}
	}
}