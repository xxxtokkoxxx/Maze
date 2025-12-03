using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Maze.CodeBase.Input
{
    public class InputStateProvider : IInputStateProvider, InputSystem_Actions.IPlayerActions
    {
        private InputSystem_Actions _inputActions = new();
        public event Action<Vector2> OnPlayerMovement;
        public event Action OnPaused;

        public void SetEnabled(bool isEnabled)
        {
            if (isEnabled)
            {
                _inputActions.Enable();
                _inputActions.Player.SetCallbacks(this);
            }
            else
            {
                _inputActions.Disable();
            }
        }

        public Vector2 GetMovementDirection()
        {
            Vector2 direction = _inputActions.Player.Move.ReadValue<Vector2>();
            return direction;
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            Vector2 direction = context.ReadValue<Vector2>();
            OnPlayerMovement?.Invoke(direction);
        }

        public void OnPause(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                OnPaused?.Invoke();
            }
        }
    }
}