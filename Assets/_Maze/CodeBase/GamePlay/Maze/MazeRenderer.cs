using _Maze.CodeBase.Infrastructure;
using UnityEngine;

namespace _Maze.CodeBase.GamePlay.Maze
{
    public class MazeRenderer : IMazeRenderer
    {
        private readonly IMazeFactory _mazeFactory;
        private readonly IMazeGenerator _mazeGenerator;
        private readonly IMonoBehavioursProvider _monoBehavioursProvider;

        public MazeRenderer(IMazeFactory mazeFactory, IMazeGenerator mazeGenerator,
            IMonoBehavioursProvider monoBehavioursProvider)
        {
            _mazeFactory = mazeFactory;
            _mazeGenerator = mazeGenerator;
            _monoBehavioursProvider = monoBehavioursProvider;
        }

        public void RenderWalls()
        {
            DestroyEnvironment();

            int width = _mazeGenerator.MazeData.Width;
            int height = _mazeGenerator.MazeData.Height;
            float cellSize = _mazeGenerator.MazeData.CellSize;

            FloorRenderer floorRenderer = _mazeFactory.GenerateFloor(width, height, _mazeGenerator.GetCentralPosition(),
                _monoBehavioursProvider.MazeSpawnPoint);
            Vector2Int centralPosition = _mazeGenerator.GetCentralPosition();
            floorRenderer.transform.localPosition = new Vector2(-centralPosition.x, -centralPosition.y);

            CreateVerticalWalls(width, height, cellSize);
            CreateHorizontalWalls(width, height, cellSize);
        }

        private void CreateVerticalWalls(int width, int height, float cellSize)
        {
            for (int x = 0; x < width + 1; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (!_mazeGenerator.VerticalWallExistsAt(x, y))
                    {
                        continue;
                    }

                    float posX = x * cellSize - cellSize / 2f;
                    float posY = y * cellSize;

                    Vector3 pos = new Vector3(posX, posY, 0f);
                    _mazeFactory.CreateWall(pos, Quaternion.identity, _monoBehavioursProvider.MazeSpawnPoint);
                }
            }
        }

        private void CreateHorizontalWalls(int width, int height, float cellSize)
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height + 1; y++)
                {
                    if (!_mazeGenerator.HorizontalWallExistsAt(x, y))
                    {
                        continue;
                    }

                    float posX = x * cellSize;
                    float posY = y * cellSize - cellSize / 2f;

                    Vector3 pos = new Vector3(posX, posY, 0f);
                    _mazeFactory.CreateWall(pos, Quaternion.Euler(0f, 0f, 90f), _monoBehavioursProvider.MazeSpawnPoint);
                }
            }
        }

        private void DestroyEnvironment()
        {
            _mazeFactory.DestroyMazeEnvironment();
        }
    }
}