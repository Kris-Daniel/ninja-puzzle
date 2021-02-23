using NinjaPuzzle.Code.Gameplay.Interfaces;
using NinjaPuzzle.Code.Gameplay.Inventory;
using NinjaPuzzle.Code.Gameplay.Managers;

namespace NinjaPuzzle.Code.Gameplay
{
	public class GameController
	{
		public GameInstance GameInstance { get; private set; }
		public RuntimeData RuntimeData { get; private set; }
		
		public EventManager EventManager { get; private set; }
		public TimeManager TimeManager { get; private set; }
		public PlayerManager PlayerManager { get; private set; }
		public InventoryManager InventoryManager { get; private set; }

		public bool IsInitialized { get; private set; }

		public GameController(GameInstance gameInstance, IGameSaveData gameSaveData)
		{
			GameInstance = gameInstance;
			RuntimeData = new RuntimeData(gameSaveData);
			
			InventoryManager = new InventoryManager(this);
			EventManager = new EventManager(this);
			TimeManager = new TimeManager(this);
			PlayerManager = new PlayerManager(this);
		}
		
		public void SetInitialized()
		{
			if (!IsInitialized)
			{
				IsInitialized = true;
				EventManager.OnGameInitialized.Invoke(this);
			}
		}
		
		public void Start()
		{
		}

		public void Update()
		{
			PlayerManager.Update();
			EventManager.Update();
			TimeManager.Update();
		}
	}
}