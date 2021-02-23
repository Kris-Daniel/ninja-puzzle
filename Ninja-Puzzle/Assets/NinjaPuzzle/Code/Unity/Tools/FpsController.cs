using System;
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

		[HideInInspector] public bool canMove = true;

		private void Awake()
		{
			m_inputManager = NinjaPuzzleApp.Instance.UnityGameInstance.InputManager;
		}

		void Start()
		{
			m_characterController = GetComponent<CharacterController>();

			// Lock cursor
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}

		void Update()
		{
			// We are grounded, so recalculate move direction based on axes
			Vector3 forward = transform.TransformDirection(Vector3.forward);
			Vector3 right = transform.TransformDirection(Vector3.right);
			// Press Left Shift to run
			bool isRunning = m_inputManager.Events[EButtonEvent.OnRun].IsPressed;
			float curSpeedX = canMove ? (isRunning ? runningSpeed : walkingSpeed) * m_inputManager.Axis.y : 0;
			float curSpeedY = canMove ? (isRunning ? runningSpeed : walkingSpeed) *  m_inputManager.Axis.x : 0;
			float movementDirectionY = m_moveDirection.y;
			m_moveDirection = (forward * curSpeedX) + (right * curSpeedY);

			if (m_inputManager.Events[EButtonEvent.OnJump].IsPressed && canMove && m_characterController.isGrounded)
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