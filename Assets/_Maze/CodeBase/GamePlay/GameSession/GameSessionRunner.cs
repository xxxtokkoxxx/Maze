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
        private readonly IPlayerFactory _playerFactory;
        private readonly ICameraFollowSystem _cameraFollowSystem;
        private readonly IGamePlayProcessor _gamePlayProcessor;
        private readonly IMonoBehavioursProvider _monoBehavioursProvider;
        private readonly IGameRuntimeDataContainer _gameRuntimeDataContainer;
        private readonly IGamePauseProcessor _gamePauseProcessor;

        public GameSessionRunner(IPlayerFactory playerFactory,
            ICameraFollowSystem cameraFollowSystem,
            IGamePlayProcessor gamePlayProcessor,
            IMonoBehavioursProvider monoBehavioursProvider,
            IGameRuntimeDataContainer gameRuntimeDataContainer,
            IGamePauseProcessor gamePauseProcessor)
        {
            _playerFactory = playerFactory;
            _cameraFollowSystem = cameraFollowSystem;
            _gamePlayProcessor = gamePlayProcessor;
            _monoBehavioursProvider = monoBehavioursProvider;
            _gameRuntimeDataContainer = gameRuntimeDataContainer;
            _gamePauseProcessor = gamePauseProcessor;
        }

        public async void StartGame(GameProgressData data, bool loadGameProgressData = false)
        {
            await _playerFactory.LoadPlayerReference();

            _gamePauseProcessor.Initialize();
            ShiftMazeSpawnPoint(data.MazeData);

            IPlayer player = _playerFactory.CreatePlayer(Vector2.zero, _monoBehavioursProvider.PlayerSpawnPoint);

            _cameraFollowSystem.Initialize(player.View.transform);
            _gamePlayProcessor.Run();
        }

        public void RestartGame()
        {
            ResetPlayerProgress();
            _gamePlayProcessor.Reset();

            IPlayer player = _playerFactory.GetPlayer();

            if (player == null)
            {
                player = _playerFactory.CreatePlayer(Vector2.zero, _monoBehavioursProvider.MazeSpawnPoint);
                _cameraFollowSystem.Initialize(player.View.transform);
            }
        }

        public void EndGame()
        {
            _cameraFollowSystem.Disable();
            _gamePlayProcessor.Stop();

            _playerFactory.DestroyPlayerView();
            _gamePauseProcessor.Dispose();
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