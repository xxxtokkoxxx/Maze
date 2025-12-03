using System.Collections.Generic;
using System.Threading.Tasks;
using _Maze.CodeBase.Infrastructure.ResourcesManagement;
using UnityEngine;

namespace _Maze.CodeBase.GamePlay.Maze
{
    public class MazeFactory : IMazeFactory
    {
        private GameObject _wallReference;
        private List<GameObject> _createdWalls = new();

        public async Task LoadReferences()
        {
            ResourceRequest resourceRequest = Resources.LoadAsync<GameObject>(AssetsDataPath.Wall);
            await resourceRequest;
            _wallReference = resourceRequest.asset as GameObject;
        }

        public GameObject CreateWall(Vector3 position, Quaternion rotation, Transform parent)
        {
            GameObject wall = Object.Instantiate(_wallReference, position, rotation, parent);
            wall.transform.localPosition = position;
            _createdWalls.Add(wall);
            return wall;
        }

        public void ReleaseResources()
        {
            //TODO:release
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