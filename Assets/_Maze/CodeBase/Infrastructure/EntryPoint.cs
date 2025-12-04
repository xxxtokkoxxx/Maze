using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _Maze.CodeBase.Configuration;
using _Maze.CodeBase.UI;
using UnityEngine;
using VContainer;

namespace _Maze.CodeBase.Infrastructure
{
    public class EntryPoint : MonoBehaviour
    {
        private IUIService _uiService;
        private IUIViewsFactory _uiViewsFactory;
        private IEnumerable<IViewController> _viewControllers;
        private IGameConfiguration _gameConfiguration;

        [Inject]
        public void Inject(IUIService uiService, IUIViewsFactory uiViewsFactory,
            IEnumerable<IViewController> viewControllers, IGameConfiguration gameConfiguration)
        {
            _gameConfiguration = gameConfiguration;
            _viewControllers = viewControllers;
            _uiViewsFactory = uiViewsFactory;
            _uiService = uiService;
        }

        private async void Start()
        {
            await Task.WhenAll(_uiViewsFactory.LoadViews(), _gameConfiguration.LoadConfiguration());

            _uiService.Initialize(_viewControllers.ToArray());
            _uiService.ShowWindow(ViewType.MainMenu);
        }
    }
}