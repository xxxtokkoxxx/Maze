using _Maze.CodeBase.GamePlay.Maze;
using _Maze.CodeBase.GamePlay.Pause;
using _Maze.CodeBase.GamePlay.Player;
using _Maze.CodeBase.Infrastructure;
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

        private readonly IInputStateProvider _inputStateProvider;
        private readonly IUIService _uiService;
        private readonly IGameRuntimeDataContainer _gameRuntimeDataContainer;
        private readonly IHeadsUpDisplay _headsUpDisplay;
        private readonly IGamePauseProcessor _pauseProcessor;
        private readonly IGameplayEventBus _gameplayEventBus;
        private readonly IPlayerFactory _playerFactory;

        public GamePlayProcessor(IInputStateProvider inputStateProvider,
            IUIService uiService,
            IGameRuntimeDataContainer gameRuntimeDataContainer,
            IHeadsUpDisplay headsUpDisplay,
            IGamePauseProcessor pauseProcessor,
            IGameplayEventBus gameplayEventBus,
            IPlayerFactory playerFactory)
        {
            _inputStateProvider = inputStateProvider;
            _uiService = uiService;
            _gameRuntimeDataContainer = gameRuntimeDataContainer;
            _headsUpDisplay = headsUpDisplay;
            _pauseProcessor = pauseProcessor;
            _gameplayEventBus = gameplayEventBus;
            _playerFactory = playerFactory;
        }

        public void Run()
        {
            if (_isEnabled)
                return;

            _uiService.ShowWindow(ViewType.Hud);
            _isEnabled = true;
            _elapsedTime = _gameRuntimeDataContainer.GetSessionTime();
            _pauseProcessor.AddPausable(this);
            _inputStateProvider.SetEnabled(true);
            _gameplayEventBus.Subscribe<PlayerDeathMessage>(OnPlayerDeath);
            IPlayer player = _playerFactory.GetPlayer();
            player.ResetPosition();
        }

        public void Reset()
        {
            _elapsedTime = 0;
            _headsUpDisplay.UpdateTimer(0);
            _headsUpDisplay.UpdateStepsCount(0);
            _inputStateProvider.SetEnabled(true);
            IPlayer player = _playerFactory.GetPlayer();
            player.ResetPosition();
        }

        public void Stop()
        {
            _isEnabled = false;
            _pauseProcessor.RemovePausable(this);
            _gameplayEventBus.UnSubscribe<PlayerDeathMessage>(OnPlayerDeath);
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