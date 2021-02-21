using NinjaPuzzle.Code.Gameplay.Managers;

namespace NinjaPuzzle.Code.Gameplay
{
	public class GameController
	{
		public GameInstance GameInstance { get; private set; }
		public PlayerManager PlayerManager { get; private set; }
		public EventManager EventManager { get; private set; }

		public bool IsInitialized { get; private set; }

		public GameController(GameInstance gameInstance)
		{
			GameInstance = gameInstance;
			PlayerManager = new PlayerManager(this);
			EventManager = new EventManager(this);
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
		}
	}
}