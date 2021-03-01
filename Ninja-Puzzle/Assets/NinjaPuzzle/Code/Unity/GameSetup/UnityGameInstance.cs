using NinjaPuzzle.Code.Gameplay;
using NinjaPuzzle.Code.UI.Uxml.Mixins;
using NinjaPuzzle.Code.Unity.Managers;

namespace NinjaPuzzle.Code.Unity.GameSetup
{
	public sealed class UnityGameInstance
	{
		public ScriptableDataManager ScriptableDataManager { get; private set; }
		public GameSaveManager GameSaveManager { get; private set; }
		public GameInstance Game { get; private set; }
		public VfxManager VfxManager { get; private set; }
		public InputManager InputManager { get; private set; }
		public InventoryManager InventoryManager { get; private set; }
		public PageXml PageXml { get; set; }
		
		public UnityGameInstance()
		{
			ScriptableDataManager = new ScriptableDataManager(this);
			GameSaveManager = new GameSaveManager(this);
			VfxManager = new VfxManager(this);
			InputManager = new InputManager(this);
			InventoryManager = new InventoryManager(this);
		}

		public void CreateGameInstance()
		{
			Game = new GameInstance(GameSaveManager.CurrentGameSave);
		}
		
		public void Update()
		{
			InputManager.Update();
			VfxManager.Update();
			GameSaveManager.Update();
		}
	}
}