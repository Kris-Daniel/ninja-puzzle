using System;

namespace NinjaPuzzle.Code.Gameplay.Managers
{
	public class EventManager : AGameManager
	{
		public Action<GameController> OnGameInitialized { get; set; }
		
		public EventManager(GameController gameController) : base(gameController) { }
	}
}