using Cysharp.Threading.Tasks;

namespace _Maze.CodeBase.Infrastructure.StateMachine
{
    public interface IGameStateMachine
    {
        UniTask Enter<TState>() where TState : IState;
        void RegisterState(IState state);
        void UnregisterState(IState state);
    }
}