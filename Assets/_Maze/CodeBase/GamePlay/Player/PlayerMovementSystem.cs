using System;
using _Maze.CodeBase.Extensions;
using _Maze.CodeBase.GamePlay.Maze;
using _Maze.CodeBase.GamePlay.Pause;
using _Maze.CodeBase.Input;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using VContainer.Unity;

namespace _Maze.CodeBase.GamePlay.Player
{
    public class PlayerMovementSystem : IPlayerMovementSystem, IPauseable, IInitializable, IDisposable
    {
        public event Action<Vector2Int> OnMoved;

        private float _moveTimer;
        private float _moveDuration = 0.35f;

        private PlayerView _playerView;
        private Vector2Int _currentPosition;

        private readonly IInputStateProvider _inputStateProvider;
        private readonly IMazeGenerator _mazeGenerator;
        private readonly IGamePauseProcessor _gamePauseProcessor;
        private bool _isEnabled = true;

        public PlayerMovementSystem(IInputStateProvider inputStateProvider,
            IMazeGenerator mazeGenerator, IGamePauseProcessor gamePauseProcessor)
        {
            _inputStateProvider = inputStateProvider;
            _mazeGenerator = mazeGenerator;
            _gamePauseProcessor = gamePauseProcessor;
        }

        public void Initialize()
        {
            _isEnabled = true;
            _gamePauseProcessor.AddPausable(this);
            _inputStateProvider.OnPlayerMovement += OnMove;
        }

        public void Dispose()
        {
            _isEnabled = false;
            _gamePauseProcessor.RemovePausable(this);
            _inputStateProvider.OnPlayerMovement -= OnMove;
        }

        public void SetPlayerView(PlayerView playerTransform)
        {
            _playerView = playerTransform;
        }

        public void SetStartPoint(Vector2Int position)
        {
            _currentPosition = position;
        }

        private void OnMove(Vector2 direction)
        {
            RaycastHit2D result = Physics2D.Raycast(_playerView.transform.position, direction);
            if (result.collider != null)
            {
                TweenerCore<Vector3, Vector3, VectorOptions> moveAnima = _playerView.transform.DOMove(new Vector3(result.point.x, result.point.y, 0), _moveDuration);
                moveAnima.onComplete += () =>
                {
                    _playerView.SetVisualsDirection(direction.ToDirection());
                };
            }
        }

        public void SetPaused(bool isPaused)
        {
            _isEnabled = !isPaused;
        }

        public Vector2Int GetCurrentPositionPoint()
        {
            return _currentPosition;
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
                    _moveTimer = _moveDuration;
                    OnMoved?.Invoke(_currentPosition);
                }
            }
        }

        private void SetPositionOnCell(Vector2Int position)
        {
            _currentPosition = position;
            _playerView.transform.DOLocalMove(new Vector3(position.x, position.y, 0f), _moveDuration);
        }
    }
}