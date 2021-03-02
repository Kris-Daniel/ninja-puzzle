using NinjaPuzzle.Code.Unity.Helpers.Singleton;
using UnityEngine;

namespace NinjaPuzzle.Code.Unity.Camera
{
	public class MainCamera : ASingleton<MainCamera>
	{
		public UnityEngine.Camera Camera { get; private set; }

		//public CameraMovementComponent MovementComponent { get; private set; }

		protected override void Init()
		{
			base.Init();
			Camera = GetComponent<UnityEngine.Camera>();

			if (Camera != null)
			{
				//MovementComponent = Camera.GetComponent<CameraMovementComponent>();
			}
		}

		public void TurnOnCamera()
		{
			Camera.enabled = true;
		}

		public void TurnOffCamera()
		{
			Camera.enabled = false;
		}


		public Vector3 GetVectorTowardsCamera(Vector3 pos)
		{
			return (Transform.position - pos).normalized;
		}

//        public Vector3 GetPositionOnMouseDragPlane(Vector3 pos)
//        {
//            return pos + GetVectorTowardsCamera(pos) * BattleMovementManager.Instance.MoveSettings.MouseDragDistanceFromBoard;
//        }
	}
}