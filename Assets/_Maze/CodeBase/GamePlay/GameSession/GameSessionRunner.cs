using _Maze.CodeBase.GamePlay.Camera;
using _Maze.CodeBase.GamePlay.Environment;
using _Maze.CodeBase.GamePlay.Pause;
using _Maze.CodeBase.GamePlay.Player;

namespace _Maze.CodeBase.GamePlay.GameSession
{
    public class GameSessionRunner : IGameSessionRunner
    {
        private readonly IPlayerFactory _playerFactory;
        private readonly ICameraFollowSystem _cameraFollowSystem;
        private readonly IGamePlayProcessor _gamePlayProcessor;
        private readonly IGamePauseProcessor _gamePauseProcessor;
        private readonly ILevelElementsContainer _levelElementsContainer;

        public GameSessionRunner(IPlayerFactory playerFactory,
            ICameraFollowSystem cameraFollowSystem,
            IGamePlayProcessor gamePlayProcessor,
            IGamePauseProcessor gamePauseProcessor,
            ILevelElementsContainer levelElementsContainer)
        {
            _playerFactory = playerFactory;
            _cameraFollowSystem = cameraFollowSystem;
            _gamePlayProcessor = gamePlayProcessor;
            _gamePauseProcessor = gamePauseProcessor;
            _levelElementsContainer = levelElementsContainer;
        }

        public void StartGame()
        {
            _playerFactory.SetPlayerReference();
            _gamePauseProcessor.Initialize();

            IPlayer player = _playerFactory.GetPlayer();

            _cameraFollowSystem.SetTarget(player.View.transform);
            _gamePlayProcessor.Run();
        }

        public void RestartGame()
        {
            _gamePlayProcessor.Reset();
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