using System;
using Unity.VisualScripting;

namespace _Maze.CodeBase.UI
{
    public interface IUIService
    {
        void ShowWindow(ViewType viewType);
        void HideWindow(ViewType viewType);
        void UnregisterController<TView>(BaseUiController<TView> uiController) where TView : IView;
        void RegisterController<TView>(BaseUiController<TView> baseUiController) where TView : IView;
    }
}