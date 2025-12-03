using _Maze.CodeBase.Data;
using _Maze.CodeBase.GamePlay.Camera;
using _Maze.CodeBase.GamePlay.Maze;
using _Maze.CodeBase.GamePlay.Pause;
using _Maze.CodeBase.GamePlay.Player;
using _Maze.CodeBase.Infrastructure;
using _Maze.CodeBase.Input;
using _Maze.CodeBase.Progress;
using _Maze.CodeBase.UI;
using UnityEngine;

namespace _Maze.CodeBase.GamePlay.GameSession
{
    public class GameSessionRunner : IGameSessionRunner
    {
        private MazeData _mazeData;

        private readonly IMazeRenderer _mazeRenderer;
        private readonly IMazeGenerator _mazeGenerator;
        private readonly IPlayerFactory _playerFactory;
        private readonly IMazeFactory _mazeFactory;
        private readonly IPlayerMovementSystem _movementSystem;
        private readonly ICameraFollowSystem _cameraFollowSystem;
        private readonly IGamePlayProcessor _gamePlayProcessor;
        private readonly IMonoBehavioursProvider _monoBehavioursProvider;
        private readonly IInputStateProvider _inputStateProvider;
        private readonly IUIService _uiService;
        private readonly IPlayerMovementSystem _playerMovementSystem;
        private readonly IGameRuntimeData _gameRuntimeData;
        private readonly IGamePauseProcessor _gamePauseProcessor;

        public GameSessionRunner(IMazeRenderer mazeRenderer,
            IMazeGenerator mazeGenerator,
            IPlayerFactory playerFactory,
            IMazeFactory mazeFactory,
            IPlayerMovementSystem movementSystem,
            ICameraFollowSystem cameraFollowSystem,
            IGamePlayProcessor gamePlayProcessor,
            IMonoBehavioursProvider monoBehavioursProvider,
            IInputStateProvider inputStateProvider,
            IUIService uiService,
            IPlayerMovementSystem playerMovementSystem,
            IGameRuntimeData gameRuntimeData,
            IGamePauseProcessor gamePauseProcessor)
        {
            _mazeRenderer = mazeRenderer;
            _mazeGenerator = mazeGenerator;
            _playerFactory = playerFactory;
            _mazeFactory = mazeFactory;
            _movementSystem = movementSystem;
            _cameraFollowSystem = cameraFollowSystem;
            _gamePlayProcessor = gamePlayProcessor;
            _monoBehavioursProvider = monoBehavioursProvider;
            _inputStateProvider = inputStateProvider;
            _uiService = uiService;
            _playerMovementSystem = playerMovementSystem;
            _gameRuntimeData = gameRuntimeData;
            _gamePauseProcessor = gamePauseProcessor;
        }

        public async void StartGame(MazeData mazeData)
        {
            _mazeData = mazeData;

            await _mazeFactory.LoadReferences();
            await _playerFactory.LoadPlayerReference();
            _gamePauseProcessor.Initialize();
            _playerMovementSystem.Initialize();

            SetGameRuntimeData(mazeData);
            ShiftMazeSpawnPoint(mazeData);

            _mazeGenerator.GenerateMaze(mazeData);
            _mazeRenderer.RenderWalls();
            Vector2Int playerStartPos = _mazeGenerator.GetCentralPosition();
            GameObject player = _playerFactory.CreatePlayer(playerStartPos, _monoBehavioursProvider.MazeSpawnPoint);
            _movementSystem.SetTargetTransform(player.transform);
            _movementSystem.SetStartPoint(playerStartPos);
            _cameraFollowSystem.Initialize(player.transform);
            _gamePlayProcessor.Run();
            _inputStateProvider.SetEnabled(true);
        }

        public void RestartGame()
        {
            ResetPlayerProgress();
            _mazeGenerator.GenerateMaze(_mazeData);
            _mazeRenderer.RenderWalls();
            _gamePlayProcessor.Reset();
            _inputStateProvider.SetEnabled(true);

            Vector2Int playerStartPos = _mazeGenerator.GetCentralPosition();
            GameObject player = _playerFactory.GetPlayerView();

            if (player == null)
            {
                player = _playerFactory.CreatePlayer(playerStartPos, _monoBehavioursProvider.MazeSpawnPoint);
                _cameraFollowSystem.Initialize(player.transform);
            }

            _movementSystem.SetTargetTransform(player.transform);
            _movementSystem.SetStartPoint(playerStartPos);
            player.transform.localPosition = new Vector2(playerStartPos.x, playerStartPos.y);
        }

        public void EndGame()
        {
            _cameraFollowSystem.Disable();
            _gamePlayProcessor.Stop();
            _playerMovementSystem.Dispose();

            _mazeFactory.ReleaseResources();
            _playerFactory.ReleaseResources();
            _playerFactory.DestroyPlayerView();
            _gamePauseProcessor.Dispose();
            _mazeFactory.DestroyMazeEnvironment();
        }

        private void ShiftMazeSpawnPoint(MazeData mazeData)
        {
            float offsetX = -(mazeData.Width * mazeData.CellSize) / 2f;
            float offsetY = -(mazeData.Height * mazeData.CellSize) / 2f;

            _monoBehavioursProvider.MazeSpawnPoint.transform.localPosition = new Vector2(offsetX, offsetY);
        }

        private void SetGameRuntimeData(MazeData mazeData)
        {
            int seed = 1;
            PlayerProgressData playerProgressData = new PlayerProgressData();
            var gameRuntimeData = new GameProgressData(seed, playerProgressData, mazeData);
            _gameRuntimeData.SetData(gameRuntimeData);
        }

        private void ResetPlayerProgress()
        {
            _gameRuntimeData.SetStepsCount(0);
            _gameRuntimeData.SetSessionTime(0);
        }
    }
}