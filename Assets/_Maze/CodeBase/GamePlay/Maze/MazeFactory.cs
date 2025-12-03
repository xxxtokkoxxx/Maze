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

        public MazeFactory(IAssetsLoaderService assetsLoaderService)
        {
            _assetsLoaderService = assetsLoaderService;
        }

        public async Task LoadReferences()
        {
            Task<GameObject> loadingTask = _assetsLoaderService.LoadAsset(AssetsDataPath.Wall);
            await loadingTask;
            _wallReference = loadingTask.Result;
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
        }

        public void DestroyMazeEnvironment()
        {
            foreach (GameObject wall in _createdWalls)
            {
                Object.Destroy(wall.gameObject);
            }

            _createdWalls.Clear();
        }
    }
}