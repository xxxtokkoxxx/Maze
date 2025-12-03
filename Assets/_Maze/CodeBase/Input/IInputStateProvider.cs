
using UnityEngine;

namespace _Maze.CodeBase.Input
{
    public interface IInputStateProvider
    {
        void Initialize();
        Vector2 GetMovementDirection();
    }
}