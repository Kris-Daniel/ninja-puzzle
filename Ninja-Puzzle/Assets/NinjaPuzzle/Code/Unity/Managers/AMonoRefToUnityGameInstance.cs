using NinjaPuzzle.Code.Unity.GameSetup;
using UnityEngine;

namespace NinjaPuzzle.Code.Unity.Managers
{
	public class AMonoRefToUnityGameInstance : MonoBehaviour
	{
		public UnityGameInstance UnityGameInstance { get; private set; }

		protected virtual void Awake()
		{
			UnityGameInstance = NinjaPuzzleApp.Instance.UnityGameInstance;
		}
	}
}