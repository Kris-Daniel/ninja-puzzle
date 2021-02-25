using NinjaPuzzle.Code.Gameplay;
using NinjaPuzzle.Code.Unity.GameSetup;
using UnityEngine;

namespace NinjaPuzzle.Code.Unity.Managers
{
	public abstract class AUnityMonoManager : MonoBehaviour
	{
		protected UnityGameInstance UnityGameInstance;
		
		protected virtual void Awake()
		{
			UnityGameInstance = NinjaPuzzleApp.Instance.UnityGameInstance;
			UnityGameInstance.MonoManagers.AddUnique(this);
		}
	}
}