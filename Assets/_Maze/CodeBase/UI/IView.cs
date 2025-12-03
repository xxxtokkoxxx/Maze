using UnityEngine;

namespace _Maze.CodeBase.UI
{
    public interface IView
    {
        ViewType ViewType { get; }
        GameObject GameObject { get; }
    }
}