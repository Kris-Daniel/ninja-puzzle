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
			for (i = 0; i < arcArray.Length - 1; i++)
			{
				RaycastHit hit;
				Vector3 direction = arcArray[i + 1] - arcArray[i];
				if (Physics.Raycast(PointGlobalPos(arcArray[i]), direction , out hit, direction.magnitude))
				{
					hitPos = hit.point - transform.position;
					i++;
					break;
				}
			}
			
			Vector3[] rayCastedArray = new Vector3[i + 1];
			
			Array.Copy(arcArray, 0, rayCastedArray, 0, i);

			rayCastedArray[i] = hitPos ?? arcArray[i];
			
			return rayCastedArray;
		}

		Vector3 PointGlobalPos(Vector3 point)
		{
			return transform.position + point;
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
			float x = t * maxDistance;
			float y = x * Mathf.Tan(m_radianAngle) - ((m_gravity * x * x) / (2 * velocity * velocity * Mathf.Cos(m_radianAngle) * Mathf.Cos(m_radianAngle)));
			return new Vector3(x, y);
		}
	}
}



















