namespace _Maze.CodeBase.UI
{
    public interface IUIService
    {
        void Initialize(IViewController[] viewControllers);
        void ShowWindow(ViewType viewType);
        void HideWindow(ViewType viewType);
    }
}