using _Maze.CodeBase.GamePlay.Camera;
using _Maze.CodeBase.GamePlay.Pause;
using _Maze.CodeBase.GamePlay.Player;
using UnityEngine;

namespace _Maze.CodeBase.GamePlay.GameSession
{
    public class GameSessionRunner : IGameSessionRunner
    {
        private readonly IPlayerFactory _playerFactory;
        private readonly ICameraFollowSystem _cameraFollowSystem;
        private readonly IGamePlayProcessor _gamePlayProcessor;
        private readonly IGamePauseProcessor _gamePauseProcessor;

        public GameSessionRunner(IPlayerFactory playerFactory,
            ICameraFollowSystem cameraFollowSystem,
            IGamePlayProcessor gamePlayProcessor,
            IGamePauseProcessor gamePauseProcessor)
        {
            _playerFactory = playerFactory;
            _cameraFollowSystem = cameraFollowSystem;
            _gamePlayProcessor = gamePlayProcessor;
            _gamePauseProcessor = gamePauseProcessor;
            Debug.Log("call");
        }

        public void StartGame()
        {
            _playerFactory.SetPlayerReference();
            _gamePauseProcessor.Initialize();

            IPlayer player = _playerFactory.GetPlayer();

            _cameraFollowSystem.Initialize(player.View.transform);
            _gamePlayProcessor.Run();
            Debug.Log("start game");
        }

        public void RestartGame()
        {
            _gamePlayProcessor.Reset();

            IPlayer player = _playerFactory.GetPlayer();

            if (player == null)
            {
                player = _playerFactory.GetPlayer();
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
    }
}