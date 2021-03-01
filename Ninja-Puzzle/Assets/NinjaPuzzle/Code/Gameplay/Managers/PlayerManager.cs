using UnityEngine;

namespace NinjaPuzzle.Code.Gameplay.Managers
{
	public sealed class PlayerManager : AGameManager
	{
		public Player.Player Player { get; private set; }
		
		public PlayerManager(GameController gameController) : base(gameController)
		{
			Player = new Player.Player();
		}

		private void PlayerManagerInit(GameController obj)
		{
			Debug.Log("PlayerManager Initialized");
		}
	}
}