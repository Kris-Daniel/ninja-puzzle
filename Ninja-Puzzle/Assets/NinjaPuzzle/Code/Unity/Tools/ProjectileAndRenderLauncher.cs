using UnityEngine;

namespace NinjaPuzzle.Code.Unity.Tools
{
	public class ProjectileAndRenderLauncher : MonoBehaviour
	{
		[SerializeField] private Transform projectileParent;
		[SerializeField] private float gravity = -9.18f;
		[SerializeField] private int resolution = 10;
		
		private float m_h;
		private bool m_canDraw;
		private Rigidbody m_projectile;
		private Vector3? m_target;

		LineRenderer m_lineRenderer;

		private Vector3[] m_zeroPoints;

		void Start() {
			m_lineRenderer = GetComponent<LineRenderer>();
			m_lineRenderer.positionCount = resolution + 1;
			m_zeroPoints = new Vector3[resolution + 1];
		}

		void FixedUpdate()
		{
			if (m_canDraw && m_projectile && m_target != null)
			{
				DrawPath();
			}
			else
			{
				m_lineRenderer.SetPositions(m_zeroPoints);
			}
		}
		
		void Update()
		{
			if (m_canDraw && m_projectile && m_target != null)
			{
				DrawPath();
			}
			else
			{
				m_lineRenderer.SetPositions(m_zeroPoints);
			}
		}

		
		void LateUpdate()
		{
			if (m_canDraw && m_projectile && m_target != null)
			{
				DrawPath();
			}
			else
			{
				m_lineRenderer.SetPositions(m_zeroPoints);
			}
		}


		public void SetProjectile(Rigidbody projectile)
		{
			if (m_projectile)
			{
				Destroy(m_projectile.gameObject);
			}
			projectile.isKinematic = true;
			projectile.transform.SetParent(projectileParent);
			projectile.transform.localPosition = Vector3.zero;
			m_projectile = projectile;
		}
		
		public void SetTarget(Vector3 targetPos)
		{
			m_target = targetPos;
		}
		
		public void ToggleDraw(bool canDraw)
		{
			m_canDraw = canDraw;
		}

		public void Launch() {
			if (m_projectile && m_target != null)
			{
				Physics.gravity = Vector3.up * gravity;
				m_projectile.transform.SetParent(null);
				m_projectile.isKinematic = false;
				m_projectile.velocity = (m_target.Value - projectileParent.position).normalized * 20 + Vector3.up * 2;
				//m_projectile.velocity = CalculateLaunchData ().initialVelocity;
				m_projectile = null;
			}
		}

		LaunchData CalculateLaunchData()
		{
			m_h = (projectileParent.position.y > m_target.Value.y) ? 0.1f : m_target.Value.y - projectileParent.position.y;

			float displacementY = m_target.Value.y - projectileParent.position.y;
			Vector3 displacementXZ = new Vector3(m_target.Value.x - projectileParent.position.x, 0, m_target.Value.z - projectileParent.position.z);
			float time = Mathf.Sqrt(-2 * m_h / gravity) + Mathf.Sqrt(2 * (displacementY - m_h) / gravity);
			Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * m_h);
			Vector3 velocityXZ = displacementXZ / time;

			return new LaunchData(velocityXZ + velocityY * -Mathf.Sign(gravity), time);
		}

		void DrawPath() {
			LaunchData launchData = CalculateLaunchData ();
			Vector3[] points = new Vector3[resolution + 1];
			

			for (int i = 0; i <= resolution; i++) {
				float simulationTime = i / (float)resolution * launchData.timeToTarget;
				Vector3 displacement = launchData.initialVelocity * simulationTime + Vector3.up * (gravity * simulationTime * simulationTime * 0.5f);
				Vector3 drawPoint = projectileParent.position + displacement;
				points[i] = drawPoint;
			}
			
			for (var i = 0; i < points.Length - 1; i++)
			{
				Debug.DrawLine(points[i], points[i + 1], Color.blue);
			}
			
			m_lineRenderer.SetPositions(points);
		}

		readonly struct LaunchData {
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