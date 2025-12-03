using _Maze.CodeBase.GamePlay.Camera;
using _Maze.CodeBase.GamePlay.Maze;
using _Maze.CodeBase.GamePlay.Player;
using _Maze.CodeBase.Infrastructure;
using UnityEngine;

namespace _Maze.CodeBase.GamePlay.GameSession
{
    public class GameSessionRunner : IGameSessionRunner
    {
        private MazeConfiguration _mazeConfiguration;

        private readonly IMazeRenderer _mazeRenderer;
        private readonly IMazeGenerator _mazeGenerator;
        private readonly IPlayerFactory _playerFactory;
        private readonly IMazeFactory _mazeFactory;
        private readonly IPlayerMovementSystem _movementSystem;
        private readonly ICameraFollowSystem _cameraFollowSystem;
        private readonly IGamePlayProcessor _gamePlayProcessor;
        private readonly IMonoBehavioursProvider _monoBehavioursProvider;

        public GameSessionRunner(IMazeRenderer mazeRenderer,
            IMazeGenerator mazeGenerator,
            IPlayerFactory playerFactory,
            IMazeFactory mazeFactory,
            IPlayerMovementSystem movementSystem,
            ICameraFollowSystem cameraFollowSystem,
            IGamePlayProcessor gamePlayProcessor,
            IMonoBehavioursProvider monoBehavioursProvider)
        {
            _mazeRenderer = mazeRenderer;
            _mazeGenerator = mazeGenerator;
            _playerFactory = playerFactory;
            _mazeFactory = mazeFactory;
            _movementSystem = movementSystem;
            _cameraFollowSystem = cameraFollowSystem;
            _gamePlayProcessor = gamePlayProcessor;
            _monoBehavioursProvider = monoBehavioursProvider;
        }

        public async void StartGame(MazeConfiguration mazeConfiguration)
        {
            _mazeConfiguration = mazeConfiguration;

            await _mazeFactory.LoadReferences();
            await _playerFactory.LoadPlayerReference();

            ShiftMazeSpawnPoint(mazeConfiguration);
            _mazeGenerator.GenerateMaze(mazeConfiguration);
            _mazeRenderer.RenderWalls();
            Vector2Int playerStartPos = _mazeGenerator.GetCentralPosition();
            GameObject player = _playerFactory.CreatePlayer(playerStartPos, _monoBehavioursProvider.MazeSpawnPoint);
            _movementSystem.SetTargetTransform(player.transform);
            _movementSystem.SetStartPoint(playerStartPos);
            _cameraFollowSystem.Initialize(player.transform);
            _gamePlayProcessor.Initialize();
        }

        public void RestartGame()
        {
            _mazeGenerator.GenerateMaze(_mazeConfiguration);
            _mazeRenderer.RenderWalls();
            Vector2Int playerStartPos = _mazeGenerator.GetCentralPosition();
            var player = _playerFactory.GetPlayerView();

            if (player == null)
            {
                player = _playerFactory.CreatePlayer(playerStartPos, _monoBehavioursProvider.MazeSpawnPoint);
                _cameraFollowSystem.Initialize(player.transform);
            }

            _movementSystem.SetTargetTransform(player.transform);
            _movementSystem.SetStartPoint(playerStartPos);
        }

        public void EndGame()
        {
        }

        private void ShiftMazeSpawnPoint(MazeConfiguration mazeConfiguration)
        {
            float offsetX = -(mazeConfiguration.Width * mazeConfiguration.CellSize) / 2f;
            float offsetY = -(mazeConfiguration.Height * mazeConfiguration.CellSize) / 2f;

            _monoBehavioursProvider.MazeSpawnPoint.transform.localPosition = new Vector2(offsetX, offsetY);
        }
    }
}