using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace NinjaPuzzle.Code.Unity.UI
{
	public class StartScreen : MonoBehaviour
	{
		private UIDocument m_uiDocument;

		private void Awake()
		{
			m_uiDocument = GetComponent<UIDocument>();
			//InitButtons();
		}

		void InitButtons()
		{
			Button btnPlay = m_uiDocument.rootVisualElement.Q<Button>("play-btn");
			btnPlay.clicked += () => SceneManager.LoadScene(1);
			
			Button btnExit = m_uiDocument.rootVisualElement.Q<Button>("play-btn");
			btnExit.clicked += Application.Quit;
			
			print(btnPlay.hierarchy.Children().ToList()[0]);
		}
	}
}