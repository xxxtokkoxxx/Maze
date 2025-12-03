using System.Collections.Generic;
using System.Linq;
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

        [Inject]
        public void Inject(IUIService uiService, IUIViewsFactory uiViewsFactory,
            IEnumerable<IViewController> viewControllers)
        {
            _viewControllers = viewControllers;
            _uiViewsFactory = uiViewsFactory;
            _uiService = uiService;
        }

        private async void Start()
        {
            await _uiViewsFactory.LoadViews();
            _uiService.Initialize(_viewControllers.ToArray());
            _uiService.ShowWindow(ViewType.MainMenu);
        }
    }
}