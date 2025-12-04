using System;
using UnityEngine;

namespace _Maze.CodeBase.GamePlay.Player
{
    public interface IPlayerMovementSystem
    {
        public void Initialize();
        public void Dispose();
        event Action<Vector2Int> OnMove;
        void SetPlayerView(PlayerView playerView);
        Vector2Int GetCurrentPositionPoint();
        void SetStartPoint(Vector2Int position);
    }
}