using _Maze.CodeBase.GamePlay.Maze;
using UnityEngine;

namespace _Maze.CodeBase.Extensions
{
    public static class Vector2IntExtensions
    {
        public static Direction ToDirection(this Vector2Int vector)
        {
            if (vector.x == 1)
            {
                return Direction.Right;
            }

            if (vector.x == -1)
            {
                return Direction.Left;
            }

            if (vector.y == 1)
            {
                return Direction.Up;
            }

            if (vector.y == -1)
            {
                return Direction.Down;
            }

            return Direction.NotDefined;
        }
    }
}