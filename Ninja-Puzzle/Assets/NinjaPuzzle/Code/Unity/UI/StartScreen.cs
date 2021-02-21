using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace NinjaPuzzle.Code.Unity.UI
{
	public class StartScreen : MonoBehaviour
	{
		[SerializeField] private Button startButton;
		[SerializeField] private Button exitButton;

		private void Awake()
		{
			InitButtonActions();
		}

		void InitButtonActions()
		{
			startButton.onClick.AddListener(() => SceneManager.LoadScene(1));
			
			exitButton.onClick.AddListener(Application.Quit);
		}
	}
}