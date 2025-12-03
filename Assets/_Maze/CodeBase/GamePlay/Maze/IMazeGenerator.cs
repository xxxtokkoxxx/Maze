using _Maze.CodeBase.Data;
using UnityEngine;

namespace _Maze.CodeBase.GamePlay.Maze
{
    public interface IMazeGenerator
    {
        void GenerateMaze(MazeData mazeData);
        bool IsWallInFront(Vector2Int targetPosition, Vector2Int direction);
        bool VerticalWallExistsAt(int x, int y);
        bool HorizontalWallExistsAt(int x, int y);
        MazeData MazeData { get; }
        Vector2Int GetCentralPosition();
    }
}