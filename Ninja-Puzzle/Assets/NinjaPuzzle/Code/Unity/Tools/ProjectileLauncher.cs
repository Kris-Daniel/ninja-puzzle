using System.Collections;
using UnityEngine;

namespace NinjaPuzzle.Code.Unity.Tools
{
	public class ProjectileLauncher : MonoBehaviour
	{
		[SerializeField] private Transform projectileParent;
		[SerializeField] private float speed = 10;
		private Rigidbody m_projectile;
		
		public void LoadProjectile(Rigidbody projectile)
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

		public void Launch(Vector3 target)
		{
			if (m_projectile)
			{
				StartCoroutine(Move(target));
				m_projectile = null;
			}
		}

		IEnumerator Move(Vector3 target)
		{
			Rigidbody projectile = m_projectile;
			projectile.transform.SetParent(null);
			
			Vector3 dir = (target - projectileParent.transform.position).normalized;
			
			float maxDistance = speed;
			float aliveTime = 0;
					

			while (true)
			{
				Ray ray = new Ray(projectile.position, dir);
				
				Debug.DrawRay(projectile.position, dir * (maxDistance * Time.deltaTime), Color.green);
				
				if (Physics.Raycast(ray, out RaycastHit hitInfo, maxDistance * Time.deltaTime, ~LayerMask.GetMask("Ignore Raycast")))
				{
					yield return null;
					projectile.position = hitInfo.point;
					break;
				}
				
				projectile.position += dir * (speed * Time.deltaTime);
				aliveTime += Time.deltaTime;
					
				if (aliveTime > 10f)
				{
					break;
				}
					
				yield return null;
			}
		}
	}
}