namespace NinjaPuzzle.Code.Gameplay.Managers
{
	public abstract class AGameManager
	{
		public GameController GameController { get; private set; }

		protected AGameManager(GameController gameController)
		{
			GameController = gameController;
		}

		public virtual void Reset(bool isDisposing)
		{
			if (isDisposing)
			{
				GameController = null;
			}
		}

		public virtual void Update()
		{
			
		}
	}
}