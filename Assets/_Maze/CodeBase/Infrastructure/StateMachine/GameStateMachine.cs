using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Maze.CodeBase.Infrastructure.StateMachine
{
    public class GameStateMachine : IGameStateMachine
    {
        private IState _currentState;
        private Dictionary<Type, IState> _states = new();

        public async UniTask Enter<TState>() where TState : IState
        {
            _currentState = _states[typeof(TState)];
            Debug.Log($"Entering state {_currentState.GetType().Name}");
            await _currentState.Enter();
        }

        public void RegisterState(IState state)
        {
            _states.Add(state.GetType(), state);
        }

        public void UnregisterState(IState state)
        {
            _states.Remove(state.GetType());
        }
    }
}