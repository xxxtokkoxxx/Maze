using System.Threading.Tasks;
using _Maze.CodeBase.GamePlay.Environment;
using _Maze.CodeBase.Infrastructure.ResourcesManagement;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Maze.CodeBase.GamePlay.Player
{
    public class PlayerFactory : IPlayerFactory
    {
        private PlayerView _playerView;

        private readonly ILevelElementsContainer _levelElementsContainer;
        private readonly IObjectResolver _resolver;

        public PlayerFactory(ILevelElementsContainer levelElementsContainer, IObjectResolver resolver)
        {
            _levelElementsContainer = levelElementsContainer;
            _resolver = resolver;
        }

        public void SetPlayerReference()
        {
            _playerView = _levelElementsContainer.GetPlayer();
            _resolver.Resolve(_playerView);
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