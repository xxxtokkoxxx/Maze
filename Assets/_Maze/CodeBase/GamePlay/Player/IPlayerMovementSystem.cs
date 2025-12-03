using System;
using UnityEngine;

namespace _Maze.CodeBase.GamePlay.Player
{
    public interface IPlayerMovementSystem
    {
        public void Initialize();
        public void Dispose();
        event Action<Vector2Int> OnMove;
        void SetTargetTransform(Transform playerTransform);

        void SetStartPoint(Vector2Int position);
    }
}