using System;
using NinjaPuzzle.Code.Unity.Enums;
using NinjaPuzzle.Code.Unity.GameSetup;
using NinjaPuzzle.Code.Unity.Systems.Inventory;
using UnityEngine;

namespace NinjaPuzzle.Code.Unity.Managers
{
	public class EventManager : AUnityManager
	{
		public Action<Inventory> OnPlayerInventoryInit;
		public Action<EGameState, EGameState> OnChangeGameState;

		public EGameState GameState { get; private set; } = EGameState.GamePlay;

		public EventManager(UnityGameInstance unityGameInstance) : base(unityGameInstance)
		{
			//SetCallbacks();
			OnChangeGameState += UnityGameInstance.InputManager.ResetPrevData;
			Cursor.lockState = CursorLockMode.Locked;
		}

		// TODO change for enter menu with ESC
		private void SetCallbacks()
		{
			Action<EEventStage, EGameState> callback = (eventStage, gameState) =>
			{
				if (eventStage == EEventStage.Down)
				{
					OnChangeGameState?.Invoke(gameState, GameState);
					GameState = gameState;
				}
			};
			
			UnityGameInstance.InputManager.Events[EGameState.GamePlay][EButtonEvent.OnInventory].Event += eventStage =>
			{
				Cursor.lockState = CursorLockMode.Locked;
				callback?.Invoke(eventStage, EGameState.GamePlayInventory);
			};
			
			UnityGameInstance.InputManager.Events[EGameState.GamePlayInventory][EButtonEvent.OnInventory].Event += eventStage =>
			{
				Cursor.lockState = CursorLockMode.None;
				callback?.Invoke(eventStage, EGameState.GamePlay);
			};
		}
	}
}