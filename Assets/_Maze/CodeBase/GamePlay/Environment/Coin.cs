using _Maze.CodeBase.Animations;
using _Maze.CodeBase.Infrastructure;
using UnityEngine;
using VContainer;

namespace _Maze.CodeBase.GamePlay.Environment
{
    public class Coin : MonoBehaviour
    {
        [SerializeField] private CoinAnimator _coinAnimator;

        private IGameplayEventBus _gameplayEventBus;

        [Inject]
        public void Inject(IGameplayEventBus gameplayEventBus)
        {
            _gameplayEventBus = gameplayEventBus;
        }


        public void OnTriggerEnter2D(Collider2D other)
        {
            _gameplayEventBus.Publish(new PickCoinEvent());
            _coinAnimator.PlayPickCoin(true);
        }
    }
}