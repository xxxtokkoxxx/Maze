using System;
using System.Collections.Generic;
using System.Linq;

namespace _Maze.CodeBase.UI
{
    public class UIService : IUIService
    {
        private IViewController[] _controllers;
        private List<IViewController> _activeControllers = new();

        public void Initialize(IViewController[] viewControllers)
        {
            _controllers = viewControllers;
        }

        public void ShowWindow(ViewType viewType)
        {
            IViewController controller = GetUiController(viewType);
            _activeControllers.Add(controller);
            controller.Show();
        }

        public void HideWindow(ViewType viewType)
        {
            IViewController controller = GetUiController(viewType);
            _activeControllers.Remove(controller);
            controller.Hide();
        }

        private IViewController GetUiController(ViewType viewType)
        {
            IViewController controller = _controllers.FirstOrDefault(a=>a.ViewType == viewType);

            if (controller == null)
            {
                throw new NullReferenceException($"No UI controller found for view type {viewType}");
            }

            return controller;
        }
    }
}