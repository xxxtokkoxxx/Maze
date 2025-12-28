using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Maze.CodeBase.UI
{
    public class UIService : IUIService
    {
        private List<IViewController> _controllers = new();
        private List<IViewController> _activeControllers = new();

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

        public void RegisterController<TView>(BaseUiController<TView> uiController)
            where TView : IView
        {
            if (_controllers.Contains(uiController))
            {
                Debug.LogError("Cannot register controllers more than once!");
                return;
            }

            _controllers.Add(uiController);
        }

        public void UnregisterController<TView>(BaseUiController<TView> uiController)
            where TView : IView
        {
            _controllers.Remove(uiController);
        }

        private IViewController GetUiController(ViewType viewType)
        {
            IViewController controller = _controllers.FirstOrDefault(a => a.ViewType == viewType);

            if (controller == null)
            {
                throw new NullReferenceException($"No UI controller found for view type {viewType}");
            }

            return controller;
        }
    }
}