namespace NinjaPuzzle.Code.Gameplay
{
	public sealed class GameInstance
	{
		public GameController GameController { get; private set; }

		public GameInstance()
		{
			GameController = new GameController(this);
		}
		
		public void Start()
		{
			GameController.Start();
		}

		public void Update()
		{
			GameController.Update();
		}
	}
}