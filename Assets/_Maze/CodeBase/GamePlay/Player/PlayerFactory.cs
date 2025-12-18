using System.Threading.Tasks;
using _Maze.CodeBase.Infrastructure.ResourcesManagement;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Maze.CodeBase.GamePlay.Player
{
    public class PlayerFactory : IPlayerFactory
    {
        private GameObject _playerViewReference;
        private PlayerView _playerView;

        private readonly IAssetsLoaderService _assetsLoaderService;
        private readonly IObjectResolver _resolver;

        public PlayerFactory(IAssetsLoaderService assetsLoaderService, IObjectResolver resolver)
        {
            _assetsLoaderService = assetsLoaderService;
            _resolver = resolver;
        }

        public async Task LoadPlayerReference()
        {
            Task<GameObject> loadingTask = _assetsLoaderService.LoadAsset<GameObject>(AssetsDataPath.PlayerView);
            await loadingTask;
            _playerViewReference = loadingTask.Result;
        }

        public IPlayer CreatePlayer(Vector2 position, Transform parent)
        {
            _playerView = _resolver.Instantiate(_playerViewReference, position, Quaternion.identity, parent)
                .GetComponent<PlayerView>();

            _playerView.transform.localPosition = Vector2.zero;

            return _playerView;
        }

        public IPlayer GetPlayer()
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
    }
}