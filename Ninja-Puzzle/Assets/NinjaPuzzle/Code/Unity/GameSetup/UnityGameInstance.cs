using NinjaPuzzle.Code.UI.Uxml.Mixins;
using NinjaPuzzle.Code.Unity.Managers;

namespace NinjaPuzzle.Code.Unity.GameSetup
{
	public sealed class UnityGameInstance
	{
		public ScriptableDataManager ScriptableDataManager { get; private set; }
		public GameSaveManager GameSaveManager { get; private set; }
		public VfxManager VfxManager { get; private set; }
		public InputManager InputManager { get; private set; }
		public InventoryManager InventoryManager { get; private set; }
		public EventManager EventManager { get; private set; }
		public PageXml PageXml { get; set; }
		
		public UnityGameInstance()
		{
			ScriptableDataManager = new ScriptableDataManager(this);
			GameSaveManager = new GameSaveManager(this);
			VfxManager = new VfxManager(this);
			InputManager = new InputManager(this);
			EventManager = new EventManager(this);
			InventoryManager = new InventoryManager(this);
		}
		
		public void Update()
		{
			InputManager.Update();
			VfxManager.Update();
			GameSaveManager.Update();
		}
	}
}