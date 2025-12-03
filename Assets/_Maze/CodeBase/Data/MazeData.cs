using System;

namespace _Maze.CodeBase.Data
{
    [Serializable]
    public class MazeData
    {
        public MazeData(int width, int height, float cellSize, int exitsCount)
        {
            Width = width;
            Height = height;
            CellSize = cellSize;
            ExitsCount = exitsCount;
        }

        public int Width { get; set; }
        public int Height { get; set; }
        public float CellSize { get; set; }
        public int ExitsCount { get; set; }
        public int Seed { get; set; }
    }
}