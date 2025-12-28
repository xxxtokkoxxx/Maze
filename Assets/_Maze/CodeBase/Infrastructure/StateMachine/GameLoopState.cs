using _Maze.CodeBase.GamePlay.Environment;
using _Maze.CodeBase.GamePlay.GameSession;
using Cysharp.Threading.Tasks;

namespace _Maze.CodeBase.Infrastructure.StateMachine
{
    public class GameLoopState : BaseState
    {
        private readonly IGameSessionRunner _gameSessionRunner;
        private readonly ILevelElementsContainer _levelElementsContainer;

        public GameLoopState(IGameStateMachine gameStateMachine, IGameSessionRunner gameSessionRunner,
            ILevelElementsContainer levelElementsContainer) : base(gameStateMachine)
        {
            _gameSessionRunner = gameSessionRunner;
            _levelElementsContainer = levelElementsContainer;
        }

        public override UniTask Enter(object payload = null)
        {
            _levelElementsContainer.InjectDependenciesIntoLevelObjects();
            _gameSessionRunner.StartGame();
            return UniTask.CompletedTask;
        }
    }
}