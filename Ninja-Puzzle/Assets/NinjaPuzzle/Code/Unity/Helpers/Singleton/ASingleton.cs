using UnityEngine;

namespace NinjaPuzzle.Code.Unity.Helpers.Singleton
{
	[DisallowMultipleComponent]
	public abstract class ASingleton<T> : MonoBehaviour where T : MonoBehaviour
	{
		private static T m_Instance = null;

		public static T Instance
		{
			get
			{
				if (m_Instance == null)
				{
					m_Instance = FindObjectOfType<T>();
					if (m_Instance != null)
					{
						(m_Instance as ASingleton<T>).InitOnce();
					}
				}
				return m_Instance;
			}
		}

		public static bool IsSpawned => m_Instance != null;

		protected bool IsInit { get; private set; }

		private Transform _Transform;
		public Transform Transform
		{
			get
			{
				if (_Transform == null && m_Instance != null)
				{
					_Transform = m_Instance.transform;
				}

				return _Transform;
			}
		}

		protected void Awake()
		{
			if (m_Instance != null && m_Instance != this)
			{
				Debug.Log("There's already a singleton of type " + this + ", destroying copy");
				Destroy(gameObject);
				return;
			}
			m_Instance = this as T;
			_Transform = transform;
			InitOnce();
		}

		private void InitOnce()
		{
			if (IsInit)
			{
				return;
			}
			IsInit = true;
			Init();
		}

		protected virtual void Init()
		{
		}

		protected virtual void Uninit()
		{
		}

		protected virtual void OnDestroy()
		{
			if (IsInit)
			{
				Uninit();
			}
			m_Instance = null;
		}
	}
}