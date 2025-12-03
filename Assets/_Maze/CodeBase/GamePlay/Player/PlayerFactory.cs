using System.Threading.Tasks;
using _Maze.CodeBase.Infrastructure.ResourcesManagement;
using UnityEngine;

namespace _Maze.CodeBase.GamePlay.Player
{
    public class PlayerFactory : IPlayerFactory
    {
        private GameObject _playerViewReference;
        private GameObject _playerView;

        private readonly IAssetsLoaderService _assetsLoaderService;

        public PlayerFactory(IAssetsLoaderService assetsLoaderService)
        {
            _assetsLoaderService = assetsLoaderService;
        }

        public async Task LoadPlayerReference()
        {
            Task<GameObject> loadingTask = _assetsLoaderService.LoadAsset(AssetsDataPath.PlayerView);
            await loadingTask;
            _playerViewReference = loadingTask.Result;
        }

        public GameObject CreatePlayer(Vector2 position, Transform parent)
        {
            _playerView = Object.Instantiate(_playerViewReference, position, Quaternion.identity, parent);
            _playerView.transform.localPosition = position;

            return _playerView;
        }

        public GameObject GetPlayerView()
        {
            return _playerView;
        }

        public void DestroyPlayerView()
        {
            if (_playerView != null)
            {
                Object.Destroy(_playerView.gameObject);
            }
        }

        public void ReleaseResources()
        {
            _assetsLoaderService.Release(AssetsDataPath.PlayerView);
        }
    }
}