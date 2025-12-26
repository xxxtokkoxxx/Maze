using Cysharp.Threading.Tasks;

namespace _Maze.CodeBase.Infrastructure.StateMachine
{
    public interface IState
    {
        UniTask Enter();
    }
}