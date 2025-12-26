using System;
using Cysharp.Threading.Tasks;
using VContainer.Unity;

namespace _Maze.CodeBase.Infrastructure.StateMachine
{
    public abstract class BaseState : IState, IInitializable, IDisposable
    {
        protected readonly IGameStateMachine GameStateMachine;

        protected BaseState(IGameStateMachine gameStateMachine)
        {
            GameStateMachine = gameStateMachine;
        }
        
        public abstract UniTask Enter();

        public void Initialize()
        {
            GameStateMachine.RegisterState(this);
        }

        public void Dispose()
        {
            GameStateMachine.UnregisterState(this);
        }
    }
}