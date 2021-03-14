using System;
using System.Collections.Generic;
using NinjaPuzzle.Code.Unity.Enums;
using NinjaPuzzle.Code.Unity.GameSetup;
using NinjaPuzzle.Code.Unity.Helpers;
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
		OnFire1,
		OnFire2,
		OnToggleFire,
		OnInteract,
		OnWeapon1,
		OnWeapon2,
		OnRun,
		OnJump,
		OnInventory
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
		public readonly Dictionary<EGameState, Dictionary<EButtonEvent, EventData>> Events = new Dictionary<EGameState, Dictionary<EButtonEvent, EventData>>();
		public Vector2 Axis = Vector2.zero;
		public Vector2 AxisMouse = Vector2.zero;
		
		readonly Dictionary<EButtonEvent, KeyCode> m_keyCodeForButtonEvents = new Dictionary<EButtonEvent, KeyCode>
		{
			{EButtonEvent.OnFire1, KeyCode.Mouse0},
			{EButtonEvent.OnFire2, KeyCode.Mouse1},
			{EButtonEvent.OnToggleFire, KeyCode.LeftControl},
			{EButtonEvent.OnInteract, KeyCode.F},
			{EButtonEvent.OnWeapon1, KeyCode.Q},
			{EButtonEvent.OnWeapon2, KeyCode.E},
			{EButtonEvent.OnRun, KeyCode.LeftShift},
			{EButtonEvent.OnJump, KeyCode.Space},
			{EButtonEvent.OnInventory, KeyCode.Tab}
		};
		
		private const float HoldDurationDelta = 0.3f;
		private EventManager EventManager => UnityGameInstance.EventManager;
		private EGameState CurrentGameState => EventManager.GameState;

		public InputManager(UnityGameInstance unityGameInstance) : base(unityGameInstance)
		{
			ExtensionTools.MapEnum<EGameState>(gameState =>
			{
				Events.Add(gameState, new Dictionary<EButtonEvent, EventData>());
				
				ExtensionTools.MapEnum<EButtonEvent>(buttonEventType =>
				{
					Events[gameState].Add(buttonEventType, new EventData());
				});
			});
		}

		public void ResetPrevData(EGameState current, EGameState prev)
		{
			foreach (var keyValuePair in Events[prev])
			{
				keyValuePair.Value.HoldDuration = 0;
				keyValuePair.Value.IsHold = false;
				keyValuePair.Value.IsPressed = false;
			}
		}

		public override void Update()
		{
			Axis.x = Input.GetAxis("Horizontal");
			Axis.y = Input.GetAxis("Vertical");
			AxisMouse.x = Input.GetAxis("Mouse X");
			AxisMouse.y = Input.GetAxis("Mouse Y");
			
			ExtensionTools.MapEnum<EButtonEvent>(buttonEventType =>
			{
				CheckForButton(buttonEventType, m_keyCodeForButtonEvents[buttonEventType]);
			});
		}

		void CheckForButton(EButtonEvent buttonEvent, KeyCode keyCode)
		{
			if (Input.GetKeyDown(keyCode))
			{
				Events[CurrentGameState][buttonEvent].HoldDuration = 0;
				Events[CurrentGameState][buttonEvent].Event?.Invoke(EEventStage.Down);
			}

			if (Input.GetKey(keyCode))
			{
				Events[CurrentGameState][buttonEvent].HoldDuration += Time.deltaTime;
				Events[CurrentGameState][buttonEvent].IsPressed = true;
				if (Events[CurrentGameState][buttonEvent].HoldDuration > HoldDurationDelta)
				{
					Events[CurrentGameState][buttonEvent].Event?.Invoke(EEventStage.Hold);
					Events[CurrentGameState][buttonEvent].IsHold = true;
				}
			}

			if (Input.GetKeyUp(keyCode))
			{
				if (Events[CurrentGameState][buttonEvent].HoldDuration < HoldDurationDelta)
				{
					Events[CurrentGameState][buttonEvent].Event?.Invoke(EEventStage.Click);
				}
				Events[CurrentGameState][buttonEvent].Event?.Invoke(EEventStage.Up);
				Events[CurrentGameState][buttonEvent].IsPressed = false;
				Events[CurrentGameState][buttonEvent].IsHold = false;
			}
		}
	}
}