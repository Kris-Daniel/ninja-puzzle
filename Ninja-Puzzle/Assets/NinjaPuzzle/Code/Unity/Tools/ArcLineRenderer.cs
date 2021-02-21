using System;
using System.Collections.Generic;
using UnityEngine;

namespace NinjaPuzzle.Code.Unity.Tools
{
	public class ArcLineRenderer : MonoBehaviour
	{
		private LineRenderer m_lr;

		[SerializeField] private int resolution;
		[SerializeField] private float velocity;
		[SerializeField] private float angle;
		private float m_radianAngle;
		private float m_gravity;


		private void Awake()
		{
			m_lr = GetComponent<LineRenderer>();
			m_gravity = Mathf.Abs(Physics.gravity.y);
		}


		private void Start()
		{
			RenderArc();
		}

		private void Update()
		{
			RenderArc();
		}

		void RenderArc()
		{
			Vector3[] path = CalculateArcArray();
			Vector3[] rayCastedPath = RayCastedArcArray(path);
			m_lr.positionCount = rayCastedPath.Length;
			m_lr.SetPositions(rayCastedPath);
		}

		// TODO use span class
		private Vector3[] RayCastedArcArray(Vector3[] arcArray)
		{
			int i;
			Vector3? hitPos = null;
			
			for (i = 0; i < arcArray.Length; i++)
			{
				print(arcArray[i].magnitude);
				float y = arcArray[i].magnitude * Mathf.Sin(Mathf.Deg2Rad * transform.localRotation.x);
				float z = arcArray[i].magnitude * Mathf.Cos(Mathf.Deg2Rad * transform.localRotation.x);
				//arcArray[i] = arcArray[i] + new Vector3(0, y, z);
			}
			
			for (i = 0; i < arcArray.Length - 1; i++)
			{
				RaycastHit hit;
				Vector3 direction = PointGlobalPos(arcArray[i + 1]) - PointGlobalPos(arcArray[i]);
				Debug.DrawRay(PointGlobalPos(arcArray[i]), direction, Color.red);
				if (Physics.Raycast(PointGlobalPos(arcArray[i]), direction , out hit, direction.magnitude))
				{
					hitPos = hit.point;
					i++;
					break;
				}
			}
			
			Vector3[] rayCastedArray = new Vector3[i + 1];
			
			Array.Copy(arcArray, 0, rayCastedArray, 0, i);

			if (hitPos != null)
			{
				Vector3 lastPoint = Vector3.zero;
				
				Debug.DrawRay(hitPos.Value, Vector3.up, Color.green);

				Vector3 zeroHeight = hitPos.Value - transform.position;
				zeroHeight.y = 0;
				lastPoint.z = zeroHeight.magnitude;
				lastPoint.y = hitPos.Value.y - transform.position.y;
				rayCastedArray[i] = lastPoint;
			}
			else
			{
				rayCastedArray[i] = arcArray[i];
			}
			
			return rayCastedArray;
		}

		Vector3 PointGlobalPos(Vector3 point)
		{
			Vector3 pos = transform.position + transform.forward * point.z + transform.up * point.y;
			return pos;
		}

		private Vector3[] CalculateArcArray()
		{
			Vector3[] arcArray = new Vector3[resolution + 1];

			m_radianAngle = Mathf.Deg2Rad * angle;
			float maxDistance = (velocity * velocity * Mathf.Sin(2 * m_radianAngle)) / m_gravity;

			for (int i = 0; i <= resolution; i++)
			{
				float t = (float)i / (float)resolution;
				arcArray[i] = CalculateArcPoint(t, maxDistance);
			}

			return arcArray;
		}

		private Vector3 CalculateArcPoint(float t, float maxDistance)
		{
			float z = t * maxDistance;
			float y = z * Mathf.Tan(m_radianAngle) - ((m_gravity * z * z) / (2 * velocity * velocity * Mathf.Cos(m_radianAngle) * Mathf.Cos(m_radianAngle)));
			return new Vector3(0, y, z);
		}
	}
}



















