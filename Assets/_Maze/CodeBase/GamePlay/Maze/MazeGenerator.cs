using System;
using System.Collections.Generic;
using System.Linq;
using _Maze.CodeBase.Extensions;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Maze.CodeBase.GamePlay.Maze
{
    public class MazeGenerator : IMazeGenerator
    {
        private bool[,] _verticalWalls;
        private bool[,] _horizontalWalls;
        private bool[,] _visited;

        public MazeConfiguration MazeConfiguration => _mazeConfiguration;

        private Vector2Int[] _directions =
        {
            new(1, 0),
            new(-1, 0),
            new(0, 1),
            new(0, -1)
        };

        private MazeConfiguration _mazeConfiguration;

        public void GenerateMaze(MazeConfiguration mazeConfiguration)
        {
            _mazeConfiguration = mazeConfiguration;
            int width = _mazeConfiguration.Width;
            int height = _mazeConfiguration.Height;

            _verticalWalls = new bool[width + 1, height];
            _horizontalWalls = new bool[width, height + 1];
            _visited = new bool[width, height];

            int startX = Math.Max(width / 2, 0);
            int startY = Math.Max(height / 2, 0);

            InitWalls();
            CreatePath(startX, startY);
            GenerateExits();
        }

        public bool IsWallInFront(Vector2Int targetPosition, Vector2Int dir)
        {
            Direction direction = dir.ToDirection();

            switch (direction)
            {
                case Direction.Left:
                    return CanMoveLeft(targetPosition, dir);
                case Direction.Right:
                    return CanMoveRight(targetPosition, dir);
                case Direction.Up:
                    return CanMoveUp(targetPosition, dir);
                case Direction.Down:
                    return CanMoveDown(targetPosition, dir);
                default:
                    return true;
            }
        }


        public Vector2Int GetCentralPosition()
        {
            return new Vector2Int(_mazeConfiguration.Width / 2, _mazeConfiguration.Height / 2);
        }

        public bool HorizontalWallExistsAt(int x, int y)
        {
            return _horizontalWalls[x, y];
        }

        public bool VerticalWallExistsAt(int x, int y)
        {
            return _verticalWalls[x, y];
        }

        private void CreatePath(int x, int y)
        {
            _visited[x, y] = true;

            Vector2Int[] directions = ShuffleDirections();

            foreach (Vector2Int direction in directions)
            {
                int nextX = x + direction.x;
                int nextY = y + direction.y;

                if (nextX < 0 || nextY < 0 || nextX >= _mazeConfiguration.Width || nextY >= _mazeConfiguration.Height)
                {
                    continue;
                }

                if (_visited[nextX, nextY])
                {
                    continue;
                }

                RemoveWall(x, y, direction);
                CreatePath(nextX, nextY);
            }
        }

        private void RemoveWall(int x, int y, Vector2Int direction)
        {
            switch (direction.ToDirection())
            {
                case Direction.Right:
                    _verticalWalls[x + 1, y] = false;
                    break;
                case Direction.Left:
                    _verticalWalls[x, y] = false;
                    break;
                case Direction.Up:
                    _horizontalWalls[x, y + 1] = false;
                    break;
                case Direction.Down:
                    _horizontalWalls[x, y] = false;
                    break;
            }
        }

        private Vector2Int[] ShuffleDirections()
        {
            Vector2Int[] dirs = (Vector2Int[])_directions.Clone();

            for (int i = dirs.Length - 1; i > 0; i--)
            {
                int j = Random.Range(0, i + 1);
                (dirs[i], dirs[j]) = (dirs[j], dirs[i]);
            }

            return dirs;
        }

        private void GenerateExits()
        {
            List<(bool isHorizontal, int x, int y)> exitWalls = new();

            for (int x = 0; x < _mazeConfiguration.Width; x++)
            {
                exitWalls.Add((true, x, 0));
                exitWalls.Add((true, x, _mazeConfiguration.Height));
            }

            for (int y = 0; y < _mazeConfiguration.Height; y++)
            {
                exitWalls.Add((false, 0, y));
                exitWalls.Add((false, _mazeConfiguration.Width, y));
            }

            exitWalls = exitWalls.OrderBy(_ => Random.value).ToList();

            for (int i = 0; i < _mazeConfiguration.ExitsCount && i < exitWalls.Count; i++)
            {
                (bool isHorizontal, int x, int y) wall = exitWalls[i];

                if (wall.isHorizontal)
                {
                    _horizontalWalls[wall.x, wall.y] = false;
                }
                else
                {
                    _verticalWalls[wall.x, wall.y] = false;
                }
            }
        }

        private void InitWalls()
        {
            for (int x = 0; x < _mazeConfiguration.Width + 1; x++)
            {
                for (int y = 0; y < _mazeConfiguration.Height; y++)
                {
                    _verticalWalls[x, y] = true;
                }
            }

            for (int x = 0; x < _mazeConfiguration.Width; x++)
            {
                for (int y = 0; y < _mazeConfiguration.Height + 1; y++)
                {
                    _horizontalWalls[x, y] = true;
                }
            }
        }

        private bool CanMoveLeft(Vector2Int currentPosition, Vector2Int direction)
        {
            if (direction.x == -1)
            {
                if (currentPosition.x <= 0)
                {
                    return _verticalWalls[0, currentPosition.y];
                }

                return _verticalWalls[currentPosition.x, currentPosition.y];
            }

            return true;
        }

        private bool CanMoveRight(Vector2Int currentPosition, Vector2Int direction)
        {
            if (direction.x == 1)
            {
                if (currentPosition.x + 1 >= _mazeConfiguration.Width)
                {
                    return _verticalWalls[_mazeConfiguration.Width, currentPosition.y];
                }

                return _verticalWalls[currentPosition.x + 1, currentPosition.y];
            }

            return true;
        }

        private bool CanMoveUp(Vector2Int currentPosition, Vector2Int direction)
        {
            if (direction.y == 1)
            {
                if (currentPosition.y + 1 >= _mazeConfiguration.Height)
                {
                    return _horizontalWalls[currentPosition.x, _mazeConfiguration.Height];
                }

                return _horizontalWalls[currentPosition.x, currentPosition.y + 1];
            }
            return true;
        }

        private bool CanMoveDown(Vector2Int currentPosition, Vector2Int direction)
        {
            if (direction.y == -1)
            {
                if (currentPosition.y <= 0)
                {
                    return _horizontalWalls[currentPosition.x, 0];
                }

                return _horizontalWalls[currentPosition.x, currentPosition.y];
            }

            return true;
        }
    }
}