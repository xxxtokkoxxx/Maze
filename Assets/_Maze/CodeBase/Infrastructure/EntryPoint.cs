using System;
using System.Threading.Tasks;
using _Maze.CodeBase.Infrastructure.StateMachine;
using _Maze.CodeBase.UI;
using Unity.Mathematics;
using UnityEngine;
using VContainer;
using Random = System.Random;

namespace _Maze.CodeBase.Infrastructure
{
    public class EntryPoint : MonoBehaviour
    {
        private IUIService _uiService;
        private IUIViewsFactory _uiViewsFactory;
        private IGameStateMachine _gameStateMachine;

        [Inject]
        public void Inject(IUIService uiService, IUIViewsFactory uiViewsFactory, IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
            _uiViewsFactory = uiViewsFactory;
            _uiService = uiService;
        }

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        private async void Start()
        {
            await _gameStateMachine.Enter<BootstrapState>();
            _uiService.ShowWindow(ViewType.MainMenu);
        }
    }
}