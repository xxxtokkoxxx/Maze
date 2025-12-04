using System.Collections.Generic;
using System.Threading.Tasks;
using _Maze.CodeBase.Infrastructure.ResourcesManagement;
using UnityEngine;

namespace _Maze.CodeBase.GamePlay.Maze
{
    public class MazeFactory : IMazeFactory
    {
        private readonly IAssetsLoaderService _assetsLoaderService;
        private GameObject _wallReference;
        private List<GameObject> _createdWalls = new();
        private FloorRenderer _floorRendererReference;
        private FloorRenderer _floorRenderer;

        public MazeFactory(IAssetsLoaderService assetsLoaderService)
        {
            _assetsLoaderService = assetsLoaderService;
        }

        public async Task LoadReferences()
        {
            Task<GameObject> wallLoadingTask = _assetsLoaderService.LoadAsset<GameObject>(AssetsDataPath.Wall);
            Task<GameObject> floorRendererLoadingTask = _assetsLoaderService.LoadAsset<GameObject>(AssetsDataPath.FloorRenderer);

            await Task.WhenAll(wallLoadingTask, floorRendererLoadingTask);

            _wallReference = wallLoadingTask.Result;
            _floorRendererReference = floorRendererLoadingTask.Result.GetComponent<FloorRenderer>();
        }

        public GameObject CreateWall(Vector2 position, Quaternion rotation, Transform parent)
        {
            GameObject wall = Object.Instantiate(_wallReference, position, rotation, parent);
            wall.transform.localPosition = position;
            _createdWalls.Add(wall);
            return wall;
        }

        public void ReleaseResources()
        {
            _assetsLoaderService.Release(AssetsDataPath.Wall);
            _assetsLoaderService.Release(AssetsDataPath.FloorRenderer);
        }

        public void DestroyMazeEnvironment()
        {
            foreach (GameObject wall in _createdWalls)
            {
                if (wall != null)
                {
                    Object.Destroy(wall.gameObject);
                }
            }

            if (_floorRenderer != null)
            {
                _floorRenderer.ClearAllTiles();
            }

            _createdWalls.Clear();
        }

        public FloorRenderer GenerateFloor(int width, int height, Vector2 floorPosition, Transform parent)
        {
            if (_floorRenderer == null)
            {
                _floorRenderer = Object.Instantiate(_floorRendererReference, parent);
            }

            _floorRenderer.GenerateFloor(width, height);

            return _floorRenderer;
        }
    }
}