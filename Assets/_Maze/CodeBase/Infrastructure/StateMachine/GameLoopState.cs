using _Maze.CodeBase.GamePlay.GameSession;
using Cysharp.Threading.Tasks;

namespace _Maze.CodeBase.Infrastructure.StateMachine
{
    public class GameLoopState : BaseState
    {
        private readonly IGameSessionRunner _gameSessionRunner;

        public GameLoopState(IGameStateMachine gameStateMachine, IGameSessionRunner gameSessionRunner) : base(gameStateMachine)
        {
            _gameSessionRunner = gameSessionRunner;
        }

        public override UniTask Enter()
        {
            _gameSessionRunner.StartGame();
            return UniTask.CompletedTask;
        }
    }
}