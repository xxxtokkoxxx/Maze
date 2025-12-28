using _Maze.CodeBase.GamePlay.Environment;
using _Maze.CodeBase.GamePlay.Exit;
using _Maze.CodeBase.GamePlay.Pause;
using _Maze.CodeBase.GamePlay.Player;
using _Maze.CodeBase.Infrastructure;
using _Maze.CodeBase.Input;
using _Maze.CodeBase.UI;
using _Maze.CodeBase.UI.Hud;
using UnityEngine;

namespace _Maze.CodeBase.GamePlay.GameSession
{
    public class GamePlayProcessor : IGamePlayProcessor, IPauseable
    {
        private bool _isEnabled;

        private readonly IInputStateProvider _inputStateProvider;
        private readonly IUIService _uiService;
        private readonly IHeadsUpDisplay _headsUpDisplay;
        private readonly IGamePauseProcessor _pauseProcessor;
        private readonly IGameplayEventBus _gameplayEventBus;
        private readonly ILevelElementsContainer _levelElementsContainer;

        public GamePlayProcessor(IInputStateProvider inputStateProvider,
            IUIService uiService,
            IHeadsUpDisplay headsUpDisplay,
            IGamePauseProcessor pauseProcessor,
            IGameplayEventBus gameplayEventBus,
            ILevelElementsContainer levelElementsContainer)
        {
            _inputStateProvider = inputStateProvider;
            _uiService = uiService;
            _headsUpDisplay = headsUpDisplay;
            _pauseProcessor = pauseProcessor;
            _gameplayEventBus = gameplayEventBus;
            _levelElementsContainer = levelElementsContainer;
        }

        public void Run()
        {
            if (_isEnabled)
                return;

            _uiService.ShowWindow(ViewType.Hud);
            _isEnabled = true;
            _pauseProcessor.AddPausable(this);
            _inputStateProvider.SetEnabled(true);
            _gameplayEventBus.Subscribe<PlayerDeathMessage>(OnPlayerDeath);
            _gameplayEventBus.Subscribe<LevelCompletedMessage>(OnLevelCompleted);
            _levelElementsContainer.SaveElementsState();
        }

        public void Reset()
        {
            _headsUpDisplay.UpdateTimer(0);
            _headsUpDisplay.UpdateStepsCount(0);
            _inputStateProvider.SetEnabled(true);
            _levelElementsContainer.RestoreLevelElementsInitialState();
        }

        public void Stop()
        {
            _isEnabled = false;
            _pauseProcessor.RemovePausable(this);
            _gameplayEventBus.UnSubscribe<PlayerDeathMessage>(OnPlayerDeath);
        }

        public void SetPaused(bool isPaused)
        {
            _isEnabled = !isPaused;
        }

        private void OnLevelCompleted(LevelCompletedMessage levelCompleted)
        {
            _uiService.ShowWindow(ViewType.GameOver);
            _inputStateProvider.SetEnabled(false);
        }

        private void OnPlayerDeath(PlayerDeathMessage playerDeathMessage)
        {
            ShowGameOver();
        }

        private void ShowGameOver()
        {
            _inputStateProvider.SetEnabled(false);
            _uiService.ShowWindow(ViewType.GameOver);

            _isEnabled = false;
        }
    }
}