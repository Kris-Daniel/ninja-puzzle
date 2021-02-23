using System;
using System.Collections.Generic;
using NinjaPuzzle.Code.Gameplay;
using NinjaPuzzle.Code.Unity.GameSetup;
using UnityEngine;

namespace NinjaPuzzle.Code.Unity.Managers
{
	public enum EEventStage
	{
		Down    = 1 << 0,
		Up      = 1 << 1,
		Hold    = 1 << 2,
		Click   = 1 << 3
	}

	public enum EButtonEvent
	{
		OnFire1    = 1 << 0,
		OnFire2    = 1 << 1,
		OnInteract = 1 << 2,
		OnWeapon1  = 1 << 3,
		OnWeapon2  = 1 << 4,
		OnRun      = 1 << 5,
		OnJump     = 1 << 6
	}
	
	public class EventData
	{
		public Action<EEventStage> Event;
		public bool IsPressed;
		public bool IsHold;
		public float HoldDuration;
	}
	
	public class InputManager : AUnityManager
	{
		public readonly Dictionary<EButtonEvent, EventData> Events = new Dictionary<EButtonEvent, EventData>();
		public Vector2 Axis = Vector2.zero;
		public Vector2 AxisMouse = Vector2.zero;
		
		readonly Dictionary<EButtonEvent, KeyCode> m_keyCodeForButtonEvents = new Dictionary<EButtonEvent, KeyCode>
		{
			{EButtonEvent.OnFire1, KeyCode.Mouse0},
			{EButtonEvent.OnFire2, KeyCode.Mouse1},
			{EButtonEvent.OnInteract, KeyCode.F},
			{EButtonEvent.OnWeapon1, KeyCode.Q},
			{EButtonEvent.OnWeapon2, KeyCode.E},
			{EButtonEvent.OnRun, KeyCode.LeftShift},
			{EButtonEvent.OnJump, KeyCode.Space}
		};
		
		private const float HoldDurationDelta = 0.3f;

		public InputManager(UnityGameInstance unityGameInstance) : base(unityGameInstance)
		{
			ExtensionTools.FastMapEnum<EButtonEvent>(buttonEventType =>
			{
				Events.Add(buttonEventType, new EventData());
			});
		}
		
		public override void Update()
		{
			Axis.x = Input.GetAxis("Horizontal");
			Axis.y = Input.GetAxis("Vertical");
			AxisMouse.x = Input.GetAxis("Mouse X");
			AxisMouse.y = Input.GetAxis("Mouse Y");
			
			ExtensionTools.FastMapEnum<EButtonEvent>(buttonEventType =>
			{
				CheckForButton(buttonEventType, m_keyCodeForButtonEvents[buttonEventType]);
			});
		}

		void CheckForButton(EButtonEvent buttonEvent, KeyCode keyCode)
		{
			if (Input.GetKeyDown(keyCode))
			{
				Events[buttonEvent].HoldDuration = 0;
				Events[buttonEvent].Event?.Invoke(EEventStage.Down);
			}

			if (Input.GetKey(keyCode))
			{
				Events[buttonEvent].HoldDuration += Time.deltaTime;
				Events[buttonEvent].IsPressed = true;
				if (Events[buttonEvent].HoldDuration > HoldDurationDelta)
				{
					Events[buttonEvent].Event?.Invoke(EEventStage.Hold);
					Events[buttonEvent].IsHold = true;
				}
			}

			if (Input.GetKeyUp(keyCode))
			{
				if (Events[buttonEvent].HoldDuration < HoldDurationDelta)
				{
					Events[buttonEvent].Event?.Invoke(EEventStage.Click);
				}
				Events[buttonEvent].Event?.Invoke(EEventStage.Up);
				Events[buttonEvent].IsPressed = false;
				Events[buttonEvent].IsHold = false;
			}
		}

	}
}