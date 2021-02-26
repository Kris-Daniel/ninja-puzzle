using NinjaPuzzle.Code.Gameplay;

namespace NinjaPuzzle.Code.Unity.Managers
{
	public abstract class AUnityMonoManager : AMonoRefToUnityGameInstance
	{
		protected override void Awake()
		{
			base.Awake();
			UnityGameInstance.MonoManagers.AddUnique(this);
		}
	}
}