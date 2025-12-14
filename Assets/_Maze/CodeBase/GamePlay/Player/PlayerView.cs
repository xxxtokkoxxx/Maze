using _Maze.CodeBase.Animations;
using _Maze.CodeBase.Extensions;
using _Maze.CodeBase.GamePlay.Maze;
using UnityEngine;

namespace _Maze.CodeBase.GamePlay.Player
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private PlayerAnimator _playerAnimator;
        [SerializeField] private SpriteRenderer _playerVisuals;
        [SerializeField] private Transform _raycastOffset;

        // public Vector2 RaycastOffset => _raycastOffset.transform.position;

        public void SetMoveSpeed(Vector2Int direction)
        {
            if (direction == Vector2.zero)
            {
                _playerAnimator.PlayMove(false);
                return;
            }

            switch (direction.ToDirection())
            {
                case Direction.Left:
                    _playerVisuals.transform.rotation = Quaternion.Euler(0, 180, 0);
                    break;
                case Direction.Right:
                    _playerVisuals.transform.rotation = Quaternion.Euler(0, 0, 0);
                    break;
            }

            _playerAnimator.PlayMove(true);
        }

        public void SetVisualsDirection(Direction direction)
        {
            switch (direction)
            {
                case Direction.Left:
                    _playerVisuals.transform.localRotation = Quaternion.Euler(0, 0, -90);
                    break;
                case Direction.Right:
                    _playerVisuals.transform.localRotation = Quaternion.Euler(0, 0, 90);
                    break;
                case Direction.Down:
                    _playerVisuals.transform.localRotation = Quaternion.Euler(0, 0, 0);
                    break;
                case Direction.Up:
                    _playerVisuals.transform.localRotation = Quaternion.Euler(180, 0, 0);
                    break;
            }

        }

        private void Update()
        {

            void Draw(RaycastHit2D hit)
            {
                if (hit.collider != null)
                    Debug.DrawLine(transform.position, hit.point, Color.green);
                else
                    Debug.DrawRay(transform.position, hit.point, Color.red);
            }

            Draw(Physics2D.Raycast(transform.position, Vector2.down));
            Draw(Physics2D.Raycast(transform.position, Vector2.up));
            Draw(Physics2D.Raycast(transform.position, Vector2.left));
            Draw(Physics2D.Raycast(transform.position, Vector2.right));
        }
    }
}