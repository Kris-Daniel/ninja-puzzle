using NinjaPuzzle.Code.Gameplay.Managers;

namespace NinjaPuzzle.Code.Gameplay.Player
{
	public class Player
	{
		public PlayerManager PlayerManager { get; private set; }
		public GameController CurrentGameController => PlayerManager.GameController;
	}
}