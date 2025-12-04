using _Maze.CodeBase.GamePlay.Maze;
using _Maze.CodeBase.GamePlay.Pause;
using _Maze.CodeBase.GamePlay.Player;
using _Maze.CodeBase.Input;
using _Maze.CodeBase.Progress;
using _Maze.CodeBase.UI;
using _Maze.CodeBase.UI.Hud;
using UnityEngine;
using VContainer.Unity;

namespace _Maze.CodeBase.GamePlay.GameSession
{
    public class GamePlayProcessor : IGamePlayProcessor, ITickable, IPauseable
    {
        private float _elapsedTime;
        private bool _isEnabled;
        private int _minimalPlayerStepCount = 1;

        private readonly IPlayerMovementSystem _playerMovementSystem;
        private readonly IMazeGenerator _mazeGenerator;
        private readonly IInputStateProvider _inputStateProvider;
        private readonly IUIService _uiService;
        private readonly IGameRuntimeDataContainer _gameRuntimeDataContainer;
        private readonly IHeadsUpDisplay _headsUpDisplay;
        private readonly IGamePauseProcessor _pauseProcessor;

        public GamePlayProcessor(IPlayerMovementSystem playerMovementSystem,
            IMazeGenerator mazeGenerator,
            IInputStateProvider inputStateProvider,
            IUIService uiService,
            IGameRuntimeDataContainer gameRuntimeDataContainer,
            IHeadsUpDisplay headsUpDisplay,
            IGamePauseProcessor pauseProcessor)
        {
            _playerMovementSystem = playerMovementSystem;
            _mazeGenerator = mazeGenerator;
            _inputStateProvider = inputStateProvider;
            _uiService = uiService;
            _gameRuntimeDataContainer = gameRuntimeDataContainer;
            _headsUpDisplay = headsUpDisplay;
            _pauseProcessor = pauseProcessor;
        }

        public void Run()
        {
            if (_isEnabled)
                return;

            _uiService.ShowWindow(ViewType.Hud);
            _isEnabled = true;
            _elapsedTime = _gameRuntimeDataContainer.GetSessionTime();
            _pauseProcessor.AddPausable(this);
            _playerMovementSystem.OnMove += OnPlayerMoved;
            _gameRuntimeDataContainer.SetPlayerPosition(_playerMovementSystem.GetCurrentPositionPoint());
            _inputStateProvider.SetEnabled(true);
        }

        public void Reset()
        {
            _elapsedTime = 0;
            _headsUpDisplay.UpdateTimer(0);
            _headsUpDisplay.UpdateStepsCount(0);
            _inputStateProvider.SetEnabled(true);
        }

        public void Stop()
        {
            _isEnabled = false;
            _pauseProcessor.RemovePausable(this);
            _playerMovementSystem.OnMove -= OnPlayerMoved;
        }

        public void Tick()
        {
            if (!_isEnabled)
            {
                return;
            }

            _elapsedTime += Time.deltaTime;
            _gameRuntimeDataContainer.SetSessionTime(_elapsedTime);
            _headsUpDisplay.UpdateTimer(_elapsedTime);
        }

        public void SetPaused(bool isPaused)
        {
            _isEnabled = !isPaused;
        }

        private void OnPlayerMoved(Vector2Int currentPosition)
        {
            int stepsCount = _gameRuntimeDataContainer.AddPlayerStepsCount(_minimalPlayerStepCount);
            _headsUpDisplay.UpdateStepsCount(stepsCount);
            _gameRuntimeDataContainer.SetPlayerPosition(currentPosition);

            if (IsPlayerOutOfMaze(currentPosition))
            {
                _inputStateProvider.SetEnabled(false);
                _uiService.ShowWindow(ViewType.GameOver);

                _isEnabled = false;
            }
        }

        private bool IsPlayerOutOfMaze(Vector2Int currentPosition)
        {
            int mazeWidth = _mazeGenerator.MazeData.Width;
            int mazeHeight = _mazeGenerator.MazeData.Height;

            if (currentPosition.x < 0
                || currentPosition.x >= mazeWidth
                || currentPosition.y < 0
                || currentPosition.y >= mazeHeight)
            {
                return true;
            }

            return false;
        }
    }
}