namespace _Maze.CodeBase.UI
{
    public abstract class BaseUiController<TView> : IViewController where TView : IView
    {
        public abstract ViewType ViewType { get; }
        public abstract void Show();
        public abstract void Hide();

        protected TView View;
    }
}