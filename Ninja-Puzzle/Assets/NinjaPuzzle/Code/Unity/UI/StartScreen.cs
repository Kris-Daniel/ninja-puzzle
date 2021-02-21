using NinjaPuzzle.Code.Unity.GameSetup;
using UnityEngine;
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
			startButton.onClick.AddListener(NinjaPuzzleApp.Instance.InitializeGame);
			
			exitButton.onClick.AddListener(Application.Quit);
		}
	}
}