using UnityEngine;

namespace NinjaPuzzle.Code.Unity.Tools
{
	public class ShellCannon : MonoBehaviour
	{
		[SerializeField] Transform target;
		[SerializeField] float turnSpeed = 1;
		[SerializeField] float speed = 15;
		[SerializeField] float angleAccuracy = 3;

		public Rigidbody CurrentShellRigidBody { get; private set; }

		void Update()
		{
			Vector3 playerPos = GetPlayerPos();
			Vector3 playerPosWithMyHeight = playerPos;
			playerPosWithMyHeight.y = transform.position.y;

			Vector3 globalForward = transform.forward;
			globalForward.y = 0;
			globalForward = globalForward.normalized;
			
			Vector3 globalDirection = (playerPosWithMyHeight - transform.position).normalized;
			
			Debug.DrawRay(transform.position, globalForward, Color.red);
			Debug.DrawRay(transform.position, globalDirection, Color.green);
			
			Quaternion lookRotation = Quaternion.LookRotation(globalDirection);
			
			float? angle = CalculateAngle(true);
			if (angle != null)
			{
				lookRotation *= Quaternion.Euler(-angle.Value, 0, 0);
			}

			transform.rotation = lookRotation;
			//transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);

			if (CurrentShellRigidBody && angle != null && Vector3.Angle(globalDirection, globalForward) < angleAccuracy)
			{
				//Fire();
			}
		}

		public void LoadShellRigidBody(Rigidbody rb)
		{
			rb.transform.SetParent(transform);
			rb.transform.position = transform.position;
			rb.velocity = Vector3.zero;
			rb.isKinematic = true;
			CurrentShellRigidBody = rb;
		}

		public void Fire()
		{
			if (CurrentShellRigidBody != null)
			{
				CurrentShellRigidBody.transform.SetParent(null);
				CurrentShellRigidBody.isKinematic = false;
				CurrentShellRigidBody.velocity = speed * transform.forward;
				
				CurrentShellRigidBody = null;
			}
		}

		float? CalculateAngle(bool low)
		{
			Vector3 targetDir = GetPlayerPos() - transform.position;

			float y = targetDir.y;
			targetDir.y = 0;
			float x = targetDir.magnitude;
			float gravity = 9.81f;
			float sSqr = speed * speed;
			float underTheSqrRoot = (sSqr * sSqr) - gravity * (gravity * x * x + 2 * y * sSqr);

			if (underTheSqrRoot >= 0)
			{
				float root = Mathf.Sqrt(underTheSqrRoot);
				float highAngle = sSqr + root;
				float lowAngle = sSqr - root;

				if (low)
					return (Mathf.Atan2(lowAngle, gravity * x) * Mathf.Rad2Deg);
				else
					return (Mathf.Atan2(highAngle, gravity * x) * Mathf.Rad2Deg);
			}
			else return null;
		}

		Vector3 GetPlayerPos()
		{
			return target.position + Vector3.up * 1.2f;
		}
	}
}