using _Maze.CodeBase.Infrastructure.StateMachine;
using UnityEngine;
using VContainer;

namespace _Maze.CodeBase.Infrastructure
{
    public class EntryPoint : MonoBehaviour
    {
        private IGameStateMachine _gameStateMachine;

        [Inject]
        public void Inject(IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        private async void Start()
        {
            await _gameStateMachine.Enter<BootstrapState>();
        }
    }
}