using UnityEngine;

namespace _Maze.CodeBase.Input
{
    public class InputStateProvider : IInputStateProvider
    {
        private InputSystem_Actions _inputActions;

        public void Initialize()
        {
            _inputActions = new @InputSystem_Actions();
            _inputActions.Enable();
        }

        public Vector2 GetMovementDirection()
        {
            Vector2 direction = _inputActions.Player.Move.ReadValue<Vector2>();

            return direction;
        }
    }
}