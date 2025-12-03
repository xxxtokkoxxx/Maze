using System.Timers;
using _Maze.CodeBase.GamePlay.Maze;
using _Maze.CodeBase.GamePlay.Player;
using _Maze.CodeBase.Input;
using _Maze.CodeBase.Progress;
using _Maze.CodeBase.UI;
using _Maze.CodeBase.UI.Hud;
using UnityEngine;
using VContainer.Unity;

namespace _Maze.CodeBase.GamePlay.GameSession
{
    public class GamePlayProcessor : IGamePlayProcessor, ITickable
    {
        private float _elapsedTime;
        private bool _ssEnabled;
        private int _minimalPlayerStepCount = 1;

        private readonly IPlayerMovementSystem _playerMovementSystem;
        private readonly IMazeGenerator _mazeGenerator;
        private readonly IInputStateProvider _inputStateProvider;
        private readonly IUIService _uiService;
        private readonly IGameRuntimeData _gameRuntimeData;
        private readonly IHeadsUpDisplay _headsUpDisplay;

        public GamePlayProcessor(IPlayerMovementSystem playerMovementSystem,
            IMazeGenerator mazeGenerator,
            IInputStateProvider inputStateProvider,
            IUIService uiService,
            IGameRuntimeData gameRuntimeData,
            IHeadsUpDisplay headsUpDisplay)
        {
            _playerMovementSystem = playerMovementSystem;
            _mazeGenerator = mazeGenerator;
            _inputStateProvider = inputStateProvider;
            _uiService = uiService;
            _gameRuntimeData = gameRuntimeData;
            _headsUpDisplay = headsUpDisplay;
        }

        public void Run()
        {
            if (_ssEnabled)
                return;

            _ssEnabled = true;
            _elapsedTime = _gameRuntimeData.GetSessionTime();

            _playerMovementSystem.OnMove += OnPlayerMoved;
        }

        public void Reset()
        {
            _elapsedTime = 0;
            _headsUpDisplay.UpdateTimer(0, 0);
            _headsUpDisplay.UpdateStepsCount(0);
        }

        public void Stop()
        {
            _ssEnabled = false;
            _playerMovementSystem.OnMove -= OnPlayerMoved;
        }

        public void Tick()
        {
            if (!_ssEnabled)
            {
                return;
            }

            _elapsedTime += Time.deltaTime;

            int minutes = Mathf.FloorToInt(_elapsedTime / 60f);
            int seconds = Mathf.FloorToInt(_elapsedTime % 60f);

            Debug.Log(_elapsedTime);
            _headsUpDisplay.UpdateTimer(minutes, seconds);
        }

        private void OnPlayerMoved(Vector2Int currentPosition)
        {
            if (IsPlayerOutOfMaze(currentPosition))
            {
                _inputStateProvider.SetEnabled(false);
                _gameRuntimeData.SetSessionTime(_elapsedTime);
                _uiService.ShowWindow(ViewType.GameOver);
                Debug.Log("ELAPSED TIME GAME OVER " + _elapsedTime);
                _ssEnabled = false;
            }
            else
            {
                int stepsCount = _gameRuntimeData.AddPlayerStepsCount(_minimalPlayerStepCount);
                _headsUpDisplay.UpdateStepsCount(stepsCount);
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