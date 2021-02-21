using System;

namespace NinjaPuzzle.Code.Gameplay.Managers
{
	public class EventManager : AGameManager
	{
		public Action<GameController> OnGameInitialized { get; private set; }
		
		public EventManager(GameController gameController) : base(gameController) { }
	}
}