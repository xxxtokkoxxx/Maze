using _Maze.CodeBase.Animations;
using _Maze.CodeBase.Infrastructure;
using _Maze.CodeBase.LayersAndTahs;
using UnityEngine;
using VContainer;

namespace _Maze.CodeBase.GamePlay.Environment
{
    public class Coin : MonoBehaviour, ILevelElement
    {
        [SerializeField] private CoinAnimator _coinAnimator;
        private bool _isPickedUp;

        private IGameplayEventBus _gameplayEventBus;

        [Inject]
        public void Inject(IGameplayEventBus gameplayEventBus)
        {
            _gameplayEventBus = gameplayEventBus;
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            if (!_isPickedUp && other.CompareTag(Tags.Player))
            {
                _gameplayEventBus.Publish(new PickCoinEvent());
                _coinAnimator.PlayPickCoin(true);
                _isPickedUp = true;
            }
        }

        public void SaveState()
        {
            _isPickedUp = false;
        }

        public void RestoreState()
        {
            if (_isPickedUp)
            {
                _coinAnimator.PlayPickCoin(false);
            }

            _isPickedUp = false;
        }
    }
}