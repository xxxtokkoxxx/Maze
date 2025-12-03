using System;
using _Maze.CodeBase.GamePlay.Maze;
using _Maze.CodeBase.Input;
using UnityEngine;
using VContainer.Unity;

namespace _Maze.CodeBase.GamePlay.Player
{
    public class PlayerMovementSystem : IPlayerMovementSystem, ITickable
    {
        public event Action<Vector2Int> OnMove;

        private float _moveCooldown = 0.15f;
        private float _moveTimer = 0f;

        private Transform _playerTransform;
        private Vector2Int _currentPosition;

        private readonly IInputStateProvider _inputStateProvider;
        private readonly IMazeGenerator _mazeGenerator;

        public PlayerMovementSystem(IInputStateProvider inputStateProvider, IMazeGenerator mazeGenerator)
        {
            _inputStateProvider = inputStateProvider;
            _mazeGenerator = mazeGenerator;
        }

        public void SetTargetTransform(Transform playerTransform)
        {
            _playerTransform = playerTransform;
        }

        public void SetStartPoint(Vector2Int position)
        {
            _currentPosition = position;
        }

        public void Tick()
        {

            if (_playerTransform != null)
            {
                _moveTimer -= Time.deltaTime;

                if (_moveTimer > 0f)
                {
                    return;
                }

                Vector2 pos = _inputStateProvider.GetMovementDirection();
                Vector2Int transformedPos = new Vector2Int((int) pos.x, (int) pos.y);
                MoveTo(transformedPos);
            }
        }

        private void SetPositionOnCell(Vector2Int position)
        {
            _currentPosition = position;
            _playerTransform.localPosition = new Vector3(position.x, position.y, 0f);
        }

        private void MoveTo(Vector2Int direction)
        {
            if (direction != Vector2Int.zero)
            {
                if (!_mazeGenerator.IsWallInFront(_currentPosition, direction))
                {
                    Vector2Int nextPosition = new Vector2Int(_currentPosition.x + direction.x,
                        _currentPosition.y + direction.y);
                    SetPositionOnCell(nextPosition);
                    _moveTimer = _moveCooldown;
                    OnMove?.Invoke(_currentPosition);
                }
            }
        }
    }
}