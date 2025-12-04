using System;
using _Maze.CodeBase.GamePlay.Maze;
using _Maze.CodeBase.GamePlay.Pause;
using _Maze.CodeBase.Input;
using DG.Tweening;
using UnityEngine;
using VContainer.Unity;

namespace _Maze.CodeBase.GamePlay.Player
{
    public class PlayerMovementSystem : IPlayerMovementSystem, ITickable, IPauseable
    {
        private static float _moveDuration = 0.5f;
        public event Action<Vector2Int> OnMove;

        private float _moveCooldown = _moveDuration;
        private float _moveTimer;

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
        }

        public void Dispose()
        {
            _isEnabled = false;
            _gamePauseProcessor.RemovePausable(this);
        }

        public void SetPlayerView(PlayerView playerTransform)
        {
            _playerView = playerTransform;
        }

        public void SetStartPoint(Vector2Int position)
        {
            _currentPosition = position;
        }

        public void Tick()
        {
            if (_isEnabled && _playerView != null)
            {
                Vector2 mousePosition = _inputStateProvider.GetMouseGridDirection(_playerView.transform.position);
                Vector2 pos;

                if (mousePosition != Vector2Int.zero)
                {
                    pos = mousePosition;
                }
                else
                {
                    pos = _inputStateProvider.GetMovementDirection();;
                }

                Vector2Int transformedPos = new Vector2Int((int) pos.x, (int) pos.y);
                _playerView.SetMoveSpeed(transformedPos);

                _moveTimer -= Time.deltaTime;

                if (_moveTimer > 0f)
                {
                    return;
                }

                MoveTo(transformedPos);
            }
        }

        public void SetPaused(bool isPaused)
        {
            _isEnabled = !isPaused;
        }

        private void SetPositionOnCell(Vector2Int position)
        {
            _currentPosition = position;
            _playerView.transform.DOLocalMove(new Vector3(position.x, position.y, 0f), _moveDuration);
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