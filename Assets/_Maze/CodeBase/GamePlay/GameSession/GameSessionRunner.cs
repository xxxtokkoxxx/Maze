using _Maze.CodeBase.Data;
using _Maze.CodeBase.GamePlay.Camera;
using _Maze.CodeBase.GamePlay.Maze;
using _Maze.CodeBase.GamePlay.Pause;
using _Maze.CodeBase.GamePlay.Player;
using _Maze.CodeBase.Infrastructure;
using _Maze.CodeBase.Progress;
using UnityEngine;

namespace _Maze.CodeBase.GamePlay.GameSession
{
    public class GameSessionRunner : IGameSessionRunner
    {
        private readonly IMazeRenderer _mazeRenderer;
        private readonly IMazeGenerator _mazeGenerator;
        private readonly IPlayerFactory _playerFactory;
        private readonly IMazeFactory _mazeFactory;
        private readonly IPlayerMovementSystem _movementSystem;
        private readonly ICameraFollowSystem _cameraFollowSystem;
        private readonly IGamePlayProcessor _gamePlayProcessor;
        private readonly IMonoBehavioursProvider _monoBehavioursProvider;
        private readonly IPlayerMovementSystem _playerMovementSystem;
        private readonly IGameRuntimeDataContainer _gameRuntimeDataContainer;
        private readonly IGamePauseProcessor _gamePauseProcessor;

        public GameSessionRunner(IMazeRenderer mazeRenderer,
            IMazeGenerator mazeGenerator,
            IPlayerFactory playerFactory,
            IMazeFactory mazeFactory,
            IPlayerMovementSystem movementSystem,
            ICameraFollowSystem cameraFollowSystem,
            IGamePlayProcessor gamePlayProcessor,
            IMonoBehavioursProvider monoBehavioursProvider,
            IPlayerMovementSystem playerMovementSystem,
            IGameRuntimeDataContainer gameRuntimeDataContainer,
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
            _playerMovementSystem = playerMovementSystem;
            _gameRuntimeDataContainer = gameRuntimeDataContainer;
            _gamePauseProcessor = gamePauseProcessor;
        }

        public async void StartGame(GameProgressData data, bool loadGameProgressData = false)
        {
            await _mazeFactory.LoadReferences();
            await _playerFactory.LoadPlayerReference();

            _gamePauseProcessor.Initialize();
            _playerMovementSystem.Initialize();

            ShiftMazeSpawnPoint(data.MazeData);
            _mazeGenerator.GenerateMaze(data.MazeData);
            _mazeRenderer.RenderWalls();

            Vector2Int playerPos = loadGameProgressData
                ? new Vector2Int(data.PlayerProgress.PositionX, data.PlayerProgress.PositionY)
                : _mazeGenerator.GetCentralPosition();

            PlayerView player = _playerFactory.CreatePlayer(playerPos, _monoBehavioursProvider.MazeSpawnPoint);

            _movementSystem.SetPlayerView(player);
            _movementSystem.SetStartPoint(playerPos);
            _cameraFollowSystem.Initialize(player.transform);
            _gamePlayProcessor.Run();
        }

        public void RestartGame()
        {
            ResetPlayerProgress();
            _mazeGenerator.GenerateMaze(_gameRuntimeDataContainer.GetGameProgressData().MazeData);
            _mazeRenderer.RenderWalls();
            _gamePlayProcessor.Reset();

            Vector2Int playerStartPos = _mazeGenerator.GetCentralPosition();
            PlayerView player = _playerFactory.GetPlayerView();

            if (player == null)
            {
                player = _playerFactory.CreatePlayer(playerStartPos, _monoBehavioursProvider.MazeSpawnPoint);
                _cameraFollowSystem.Initialize(player.transform);
            }

            _movementSystem.SetPlayerView(player);
            _movementSystem.SetStartPoint(playerStartPos);
            player.transform.localPosition = new Vector2(playerStartPos.x, playerStartPos.y);
        }

        public void EndGame()
        {
            _cameraFollowSystem.Disable();
            _gamePlayProcessor.Stop();
            _playerMovementSystem.Dispose();

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

        private void ResetPlayerProgress()
        {
            _gameRuntimeDataContainer.SetStepsCount(0);
            _gameRuntimeDataContainer.SetSessionTime(0);
            _gameRuntimeDataContainer.SetSeed(SeedGenerator.GenerateSeed());
        }
    }
}