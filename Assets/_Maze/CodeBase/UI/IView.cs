using UnityEngine;

namespace _Maze.CodeBase.UI
{
    public interface IView
    {
        ViewType ViewType { get; }
        void Show();
        void Hide();
        GameObject GameObject { get; }
    }
}