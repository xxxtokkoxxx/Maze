using _Maze.CodeBase.GamePlay.Player;
using _Maze.CodeBase.Infrastructure;
using _Maze.CodeBase.LayersAndTahs;
using UnityEngine;
using VContainer;

namespace _Maze.CodeBase.GamePlay.Exit
{
    public class ExitView : MonoBehaviour
    {
        [SerializeField] private ExitAnimator _exitAnimator;

        private bool _isOpened = true;
        private IGameplayEventBus _gameplayEventBus;

        [Inject]
        public void Inject(IGameplayEventBus gameplayEventBus)
        {
            _gameplayEventBus = gameplayEventBus;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!_isOpened || !other.CompareTag(Tags.Player))
            {
                return;
            }

            _gameplayEventBus.Publish(new LevelCompletedMessage());
            IPlayer player = other.GetComponent<IPlayer>();
            player.PlayLevelCompletionAnimation();
        }

        public void Open()
        {
            _exitAnimator.Open();
            _isOpened = true;
        }

        public void Close()
        {
            _exitAnimator.Close();
            _isOpened = false;
        }
    }
}