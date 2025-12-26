using _Maze.CodeBase.GamePlay.Environment;
using UnityEngine;

namespace _Maze.CodeBase.GamePlay.Player
{
    public class PlayerFactory : IPlayerFactory
    {
        private PlayerView _playerView;

        private readonly ILevelElementsContainer _levelElementsContainer;

        public PlayerFactory(ILevelElementsContainer levelElementsContainer)
        {
            _levelElementsContainer = levelElementsContainer;
        }

        public void SetPlayerReference()
        {
            _playerView = _levelElementsContainer.GetPlayer();
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