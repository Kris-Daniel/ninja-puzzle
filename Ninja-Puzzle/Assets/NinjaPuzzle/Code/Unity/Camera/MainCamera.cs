using NinjaPuzzle.Code.Unity.Helpers.Singleton;
using UnityEngine;

namespace NinjaPuzzle.Code.Unity.Camera
{
	public class MainCamera : ASingleton<MainCamera>
	{
		public UnityEngine.Camera Camera { get; private set; }

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
		
		public Vector3 RayCastForward()
		{
			Vector3 result = Vector3.zero;

			Ray RayOrigin;
			RaycastHit HitInfo;
			
			RayOrigin = new Ray(transform.position, MainCamera.Instance.Camera.transform.forward);
			
			if (Physics.Raycast(RayOrigin, out HitInfo,20f))
			{
				Debug.DrawRay(RayOrigin.origin,HitInfo.point - RayOrigin.origin ,Color.yellow);
				
				result = HitInfo.point;
			}
			else
			{
				result = RayOrigin.origin + RayOrigin.direction * 60;
			}
			
			return result;
		}
	}
}