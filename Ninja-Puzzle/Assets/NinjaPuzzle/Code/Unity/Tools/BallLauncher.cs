using System;
using UnityEngine;

namespace NinjaPuzzle.Code.Unity.Tools
{
	public class BallLauncher : MonoBehaviour
	{
		public Rigidbody ball;
		public Transform target;

		public float gravity = -9.18f;
		public int resolution = 10;
		float h;


		LineRenderer lr;

		void Start() {
			ball.useGravity = false;
			lr = GetComponent<LineRenderer>();
			lr.positionCount = resolution + 1;
		}

		void Update() {
			if (Input.GetKeyDown (KeyCode.Space)) {
				Launch ();
			}
		}

		void LateUpdate()
		{
			DrawPath();
		}


		void Launch() {
			Physics.gravity = Vector3.up * gravity;
			ball.useGravity = true;
			ball.velocity = CalculateLaunchData ().initialVelocity;
		}

		LaunchData CalculateLaunchData()
		{
			h = ball.position.y > target.position.y ? 1 : target.position.y - ball.position.y;
			
			float displacementY = target.position.y - ball.position.y;
			Vector3 displacementXZ = new Vector3 (target.position.x - ball.position.x, 0, target.position.z - ball.position.z);
			float time = Mathf.Sqrt(-2*h/gravity) + Mathf.Sqrt(2*(displacementY - h)/gravity);
			Vector3 velocityY = Vector3.up * Mathf.Sqrt (-2 * gravity * h);
			Vector3 velocityXZ = displacementXZ / time;

			return new LaunchData(velocityXZ + velocityY * -Mathf.Sign(gravity), time);
		}

		void DrawPath() {
			LaunchData launchData = CalculateLaunchData ();
			Vector3[] points = new Vector3[resolution + 1];

			for (int i = 0; i <= resolution; i++) {
				float simulationTime = i / (float)resolution * launchData.timeToTarget;
				Vector3 displacement = launchData.initialVelocity * simulationTime + Vector3.up * (gravity * simulationTime * simulationTime * 0.5f);
				Vector3 drawPoint = ball.position + displacement;
				points[i] = ToLocalSpace(drawPoint);
			}
			lr.SetPositions(points);
		}

		Vector3 ToLocalSpace(Vector3 point)
		{
			return ball.transform.InverseTransformDirection(point - ball.position);
		}

		struct LaunchData {
			public readonly Vector3 initialVelocity;
			public readonly float timeToTarget;

			public LaunchData (Vector3 initialVelocity, float timeToTarget)
			{
				this.initialVelocity = initialVelocity;
				this.timeToTarget = timeToTarget;
			}
		
		}
	}
}