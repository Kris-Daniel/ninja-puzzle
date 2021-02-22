using NinjaPuzzle.Code.Unity.GameSetup;

namespace NinjaPuzzle.Code.Unity.Managers
{
	public abstract class AUnityManager
	{
		public UnityGameInstance UnityGameInstance;

		public AUnityManager(UnityGameInstance unityGameInstance)
		{
			UnityGameInstance = unityGameInstance;
		}
		
		public virtual void Update() {}
	}
}