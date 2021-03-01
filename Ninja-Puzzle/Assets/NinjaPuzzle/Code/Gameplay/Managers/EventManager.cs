using System;
using NinjaPuzzle.Code.Unity.Systems.Inventory;

namespace NinjaPuzzle.Code.Gameplay.Managers
{
	public class EventManager : AGameManager
	{
		public Action<GameController> OnGameInitialized { get; set; }
		public Action<Inventory> OnToggleInventory;
		
		public EventManager(GameController gameController) : base(gameController) { }
	}
}