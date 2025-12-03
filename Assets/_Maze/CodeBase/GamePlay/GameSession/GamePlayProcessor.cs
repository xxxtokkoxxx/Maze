using _Maze.CodeBase.GamePlay.Maze;
using _Maze.CodeBase.GamePlay.Player;
using UnityEngine;

namespace _Maze.CodeBase.GamePlay.GameSession
{
    public class GamePlayProcessor : IGamePlayProcessor
    {
        private readonly IPlayerMovementSystem _playerMovementSystem;
        private readonly IMazeGenerator _mazeGenerator;

        public GamePlayProcessor(IPlayerMovementSystem playerMovementSystem, IMazeGenerator mazeGenerator)
        {
            _playerMovementSystem = playerMovementSystem;
            _mazeGenerator = mazeGenerator;
        }

        public void Initialize()
        {
            _playerMovementSystem.OnMove += IsPlayerOutOfMaze;
        }

        public void Dispose()
        {
            _playerMovementSystem.OnMove -= IsPlayerOutOfMaze;
        }

        private void IsPlayerOutOfMaze(Vector2Int currentPosition)
        {
            int mazeWidth = _mazeGenerator.MazeData.Width;
            int mazeHeight = _mazeGenerator.MazeData.Height;

            if (currentPosition.x < 0
                || currentPosition.x >= mazeWidth
                || currentPosition.y < 0
                || currentPosition.y >= mazeHeight)
            {
                Debug.Log("game over");
            }
        }
    }
}