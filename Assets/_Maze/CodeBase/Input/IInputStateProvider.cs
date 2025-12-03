
using System;
using UnityEngine;

namespace _Maze.CodeBase.Input
{
    public interface IInputStateProvider
    {
        void SetEnabled(bool isEnabled);
        public event Action<Vector2> OnPlayerMovement;
        public event Action OnPaused;
        Vector2 GetMovementDirection();
        Vector2 GetMouseGridDirection(Vector2 playerPosition);
    }
}