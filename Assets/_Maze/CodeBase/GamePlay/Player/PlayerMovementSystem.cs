using System;
using _Maze.CodeBase.Extensions;
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
        private float _moveTimer;
        private bool IsPaysed;
        private IPlayer _playerView;

        private readonly IInputStateProvider _inputStateProvider;
        private readonly IGamePauseProcessor _gamePauseProcessor;
        private readonly IPlayerFactory _playerFactory;

        public PlayerMovementSystem(IInputStateProvider inputStateProvider, IGamePauseProcessor gamePauseProcessor, IPlayerFactory playerFactory)
        {
            _inputStateProvider = inputStateProvider;
            _gamePauseProcessor = gamePauseProcessor;
            _playerFactory = playerFactory;
        }

        public void Initialize()
        {
            _gamePauseProcessor.AddPausable(this);
            _inputStateProvider.OnPlayerMovement += OnMove;
        }

        public void Dispose()
        {
            _gamePauseProcessor.RemovePausable(this);
            _inputStateProvider.OnPlayerMovement -= OnMove;
        }

        private void OnMove(Vector2 direction)
        {
            if (IsPaysed)
                return;

            if (_playerView == null)
            {
                _playerView = _playerFactory.GetPlayer();
            }

            Vector3 playerPos = _playerView.View.transform.position;
            RaycastHit2D result = Physics2D.Raycast(playerPos, direction, Mathf.Infinity);
            if (result.collider != null)
            {
                float movementTime = Vector2.Distance(playerPos, result.point) * 0.05f;

                TweenerCore<Vector3, Vector3, VectorOptions> moveAnima = _playerView.View.transform
                    .DOMove(new Vector2(result.point.x - _playerView.Visuals.bounds.size.x / 2 * direction.x,
                        result.point.y - _playerView.Visuals.bounds.size.y / 2 * direction.y), movementTime);

                _playerView.PlayJumpAnimation(movementTime);
                _playerView.SetVisualsDirection(direction.ToDirection());
            }
        }

        public void SetPaused(bool isPaused)
        {
            IsPaysed = isPaused;
        }
    }
}