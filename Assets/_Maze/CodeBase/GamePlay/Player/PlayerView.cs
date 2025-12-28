using _Maze.CodeBase.Animations;
using _Maze.CodeBase.GamePlay.Environment;
using _Maze.CodeBase.GamePlay.Maze;
using _Maze.CodeBase.Infrastructure;
using UnityEngine;
using VContainer;

namespace _Maze.CodeBase.GamePlay.Player
{
    public class PlayerView : MonoBehaviour, IPlayer, ILevelElement
    {
        [SerializeField] private PlayerAnimator _playerAnimator;
        [SerializeField] private SpriteRenderer _playerVisuals;
        [SerializeField] private Transform _raycastOffset;

        private Vector2 _initialPosition;
        private Quaternion _initialRotation;

        private int _health = 1;
        private IGameplayEventBus _gameplayEventBus;

        public SpriteRenderer Visuals => _playerVisuals;

        public GameObject View => gameObject;

        [Inject]
        public void Inject(IGameplayEventBus gameplayEventBus)
        {
            _gameplayEventBus = gameplayEventBus;
        }

        public void PlayJumpAnimation(float duration)
        {
            _playerAnimator.PlayJump(duration);
        }

        public void PlayLevelCompletionAnimation()
        {
            SetVisualsDirection(Direction.Down);
            _playerAnimator.PlayDisappearance();
        }

        public void SetVisualsDirection(Direction direction)
        {
            switch (direction)
            {
                case Direction.Left:
                    transform.localRotation = Quaternion.Euler(0, 0, -90);
                    _playerVisuals.transform.localRotation = Quaternion.Euler(0, 0, 0);
                    break;
                case Direction.Right:
                    transform.localRotation = Quaternion.Euler(0, 0, 90);
                    _playerVisuals.transform.localRotation = Quaternion.Euler(0, 180, 0);
                    break;
                case Direction.Down:
                    transform.localRotation = Quaternion.Euler(0, 0, 0);
                    break;
                case Direction.Up:
                    transform.localRotation = Quaternion.Euler(180, 0, 0);
                    break;
            }
        }

        public void DoDamage(int damage)
        {
            _health -= damage;

            if (_health <= 0)
            {
                _playerAnimator.PlayDeath();
                _gameplayEventBus.Publish(new PlayerDeathMessage());
            }
        }

        public void ResetPosition()
        {
            _playerAnimator.PlayIdle();
            Visuals.color = Color.white;
            transform.localPosition = _initialPosition;
            transform.localRotation = _initialRotation;
        }

        public void SaveState()
        {
            Debug.Log("set initial position " + _initialPosition);
            _initialPosition = transform.localPosition;
            _initialRotation = transform.localRotation;
        }

        public void RestoreState()
        {
            _playerAnimator.PlayIdle();
            transform.localPosition = _initialPosition;
            transform.localRotation = _initialRotation;
            Debug.Log("restore state");
        }

#if UNITY_EDITOR

        private void OnGUI()
        {
            void Draw(RaycastHit2D hit)
            {
                if (hit.collider != null)
                    Debug.DrawLine(_raycastOffset.position, hit.point, Color.green);
                else
                    Debug.DrawRay(_raycastOffset.position, hit.point, Color.red);
            }

            Draw(Physics2D.Raycast(_raycastOffset.position, Vector2.down));
            Draw(Physics2D.Raycast(_raycastOffset.position, Vector2.up));
            Draw(Physics2D.Raycast(_raycastOffset.position, Vector2.left));
            Draw(Physics2D.Raycast(_raycastOffset.position, Vector2.right));
        }
#endif
    }
}