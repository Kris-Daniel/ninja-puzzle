using NinjaPuzzle.Code.Gameplay;
using NinjaPuzzle.Code.Unity.Managers;

namespace NinjaPuzzle.Code.Unity.GameSetup
{
	public sealed class UnityGameInstance
	{
		public GameInstance Game { get; private set; }
		public VfxManager VfxManager { get; private set; }

		public UnityGameInstance()
		{
			Game = new GameInstance();
			VfxManager = new VfxManager();
		}
	}
}