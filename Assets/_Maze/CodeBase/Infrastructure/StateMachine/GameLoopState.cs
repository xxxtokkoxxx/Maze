using Cysharp.Threading.Tasks;

namespace _Maze.CodeBase.Infrastructure.StateMachine
{
    public class GameLoopState : BaseState
    {
        public GameLoopState(IGameStateMachine gameStateMachine) : base(gameStateMachine)
        {
        }

        public override UniTask Enter()
        {
            return UniTask.CompletedTask;
        }
    }
}