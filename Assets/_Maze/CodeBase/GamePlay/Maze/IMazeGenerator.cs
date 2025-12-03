using _Maze.CodeBase.Infrastructure;
using UnityEngine;

namespace _Maze.CodeBase.GamePlay.Maze
{
    public interface IMazeGenerator
    {
        void GenerateMaze(MazeConfiguration mazeConfiguration);
        bool IsWallInFront(Vector2Int targetPosition, Vector2Int direction);
        bool VerticalWallExistsAt(int x, int y);
        bool HorizontalWallExistsAt(int x, int y);
        MazeConfiguration MazeConfiguration { get; }
        Vector2Int GetCentralPosition();
    }
}