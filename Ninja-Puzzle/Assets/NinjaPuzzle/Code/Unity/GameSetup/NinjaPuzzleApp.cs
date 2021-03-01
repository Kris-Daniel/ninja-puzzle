using NinjaPuzzle.Code.Unity.Helpers.Singleton;

namespace NinjaPuzzle.Code.Unity.GameSetup
{
	public class NinjaPuzzleApp : ASingleton<NinjaPuzzleApp>
	{
		public UnityGameInstance UnityGameInstance { get; private set; }

		protected override void Init()
		{
			UnityGameInstance = UnityGameSetupFactory.Create();
		}

		private void Update()
		{
			UnityGameInstance?.Update();
		}
	}
}