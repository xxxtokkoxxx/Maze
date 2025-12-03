using System;

namespace _Maze.CodeBase.GamePlay.Maze
{
    [Serializable]
    public class MazeConfiguration
    {
        public MazeConfiguration(int width, int height, float cellSize, int exitsCount)
        {
            Width = width;
            Height = height;
            CellSize = cellSize;
            ExitsCount = exitsCount;
        }

        public int Width { get; }
        public int Height { get; }
        public float CellSize { get; }
        public int ExitsCount { get; }
    }
}