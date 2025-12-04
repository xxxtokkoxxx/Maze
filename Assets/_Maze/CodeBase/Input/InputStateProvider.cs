using System;
using _Maze.CodeBase.Infrastructure;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Maze.CodeBase.Input
{
    public class InputStateProvider : IInputStateProvider, InputSystem_Actions.IPlayerActions
    {
        public event Action<Vector2> OnPlayerMovement;
        public event Action OnPaused;

        private InputSystem_Actions _inputActions = new();
        private readonly IMonoBehavioursProvider _monoBehavioursProvider;

        public InputStateProvider(IMonoBehavioursProvider monoBehavioursProvider)
        {
            _monoBehavioursProvider = monoBehavioursProvider;
        }

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

        public Vector2 GetMouseGridDirection(Vector2 playerPosition)
        {
            if (!Mouse.current.leftButton.isPressed || !_inputActions.Player.Move.enabled)
            {
                return Vector2.zero;
            }

            Vector3 mouseWorld = _monoBehavioursProvider.CachedCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            mouseWorld.z = 0f;

            Vector2 delta = (Vector2)mouseWorld - playerPosition;

            if (delta.magnitude < 0.1f)
            {
                return Vector2Int.zero;
            }

            if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
            {
                return delta.x > 0 ? Vector2Int.right : Vector2Int.left;
            }

            return delta.y > 0 ? Vector2Int.up : Vector2Int.down;
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