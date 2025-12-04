using System.Threading.Tasks;
using UnityEngine;

namespace _Maze.CodeBase.Configuration
{
    public interface IGameConfiguration
    {
        int MaxExists { get; }
        int MinExists { get; }
        Vector2Int MaxMazeSize { get; }
        Vector2Int MinMazeSize { get; }
        Task LoadConfiguration();
    }
}