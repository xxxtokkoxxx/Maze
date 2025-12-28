using System;
using UnityEngine;
using VContainer.Unity;

namespace _Maze.CodeBase.UI
{
    public abstract class BaseUiController<TView> : IInitializable, IDisposable, IViewController where TView : IView
    {
        protected readonly IUIService UIService;

        protected BaseUiController(IUIService uiService)
        {
            UIService = uiService;
        }

        public abstract ViewType ViewType { get; }
        public abstract void Show();
        public abstract void Hide();

        public void Initialize()
        {
            UIService.RegisterController(this);
        }

        public void Dispose()
        {
            UIService.UnregisterController(this);
        }

        protected TView View;
    }
}