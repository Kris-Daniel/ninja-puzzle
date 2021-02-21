using NinjaPuzzle.Code.Unity.Helpers.Singleton;

namespace NinjaPuzzle.Code.Unity.GameSetup
{
	public class NinjaPuzzleApp : ASingleton<NinjaPuzzleApp>
	{
		private UnityGameInstance m_unityGameInstance;
		
		protected override void Init()
		{
			m_unityGameInstance = UnityGameSetupFactory.Create();
		}
	}
}