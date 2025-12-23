using _Maze.CodeBase.Infrastructure;
using _Maze.CodeBase.Infrastructure.EventBus;
using UnityEngine;
using VContainer;

namespace _Maze.CodeBase.GamePlay.Environment
{
    public class Coin : MonoBehaviour
    {
        private IGameplayEventBus _gameplayEventBus;

        [Inject]
        public void Inject(IGameplayEventBus gameplayEventBus)
        {
            _gameplayEventBus = gameplayEventBus;
        }


        public void OnTriggerEnter2D(Collider2D other)
        {
            _gameplayEventBus.Publish(new PickCoinEvent());
        }
    }

    public class PickCoinEvent : IGameplayEvent
    {

    }
}