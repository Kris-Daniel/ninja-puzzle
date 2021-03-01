using NinjaPuzzle.Code.Unity.Enums;
using NinjaPuzzle.Code.Unity.GameSetup;
using NinjaPuzzle.Code.Unity.Managers;
using UnityEngine;

namespace NinjaPuzzle.Code.Unity.Tools
{
	[RequireComponent(typeof(CharacterController))]
	public class FpsController : MonoBehaviour
	{
		[Header("Movement Parameters")]
		public float walkingSpeed = 7.5f;
		public float runningSpeed = 11.5f;
		public float jumpSpeed = 8.0f;
		public float gravity = 20.0f;
		[Header("Camera Parameters")]
		public UnityEngine.Camera playerCamera;
		public float lookSpeed = 2.0f;
		public float lookXLimit = 45.0f;

		private CharacterController m_characterController;
		private Vector3 m_moveDirection = Vector3.zero;
		private float m_rotationX;
		
		private InputManager m_inputManager;

		private EventManager m_eventManager;

		[HideInInspector] public bool canMove = true;

		private void Awake()
		{
			m_inputManager = NinjaPuzzleApp.Instance.UnityGameInstance.InputManager;
			m_eventManager = NinjaPuzzleApp.Instance.UnityGameInstance.EventManager;
		}

		void Start()
		{
			m_characterController = GetComponent<CharacterController>();
		}

		private float m_axisY;
		private float m_axisX;

		void Update()
		{
			canMove = m_eventManager.GameState == EGameState.GamePlay;
			
			// We are grounded, so recalculate move direction based on axes
			Vector3 forward = transform.TransformDirection(Vector3.forward);
			Vector3 right = transform.TransformDirection(Vector3.right);
			// Press Left Shift to run
			bool isRunning = m_inputManager.Events[EGameState.GamePlay][EButtonEvent.OnRun].IsPressed;

			if (canMove)
			{
				m_axisY = m_inputManager.Axis.y;
				m_axisX = m_inputManager.Axis.x;
			}
			else
			{
				m_axisY = Mathf.Lerp(m_axisY, 0, Time.deltaTime * 5);
				m_axisX = Mathf.Lerp(m_axisX, 0, Time.deltaTime * 5);
			}
			
			float curSpeedX = (isRunning ? runningSpeed : walkingSpeed) * m_axisY;
			float curSpeedY = (isRunning ? runningSpeed : walkingSpeed) * m_axisX;
			float movementDirectionY = m_moveDirection.y;
			m_moveDirection = (forward * curSpeedX) + (right * curSpeedY);

			if (m_inputManager.Events[EGameState.GamePlay][EButtonEvent.OnJump].IsPressed && canMove && m_characterController.isGrounded)
			{
				m_moveDirection.y = jumpSpeed;
			}
			else
			{
				m_moveDirection.y = movementDirectionY;
			}

			// Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
			// when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
			// as an acceleration (ms^-2)
			if (!m_characterController.isGrounded)
			{
				m_moveDirection.y -= gravity * Time.deltaTime;
			}

			// Move the controller
			m_characterController.Move(m_moveDirection * Time.deltaTime);

			// Player and Camera rotation
			if (canMove)
			{
				m_rotationX += -m_inputManager.AxisMouse.y * lookSpeed;
				m_rotationX = Mathf.Clamp(m_rotationX, -lookXLimit, lookXLimit);
				playerCamera.transform.localRotation = Quaternion.Euler(m_rotationX, 0, 0);
				transform.rotation *= Quaternion.Euler(0, m_inputManager.AxisMouse.x * lookSpeed, 0);
			}
		}
	}
}