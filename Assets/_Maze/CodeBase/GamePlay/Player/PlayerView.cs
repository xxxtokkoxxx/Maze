using System;
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

        // private void Update()
        // {
        //     if (UnityEngine.Input.GetKeyDown(KeyCode.A))
        //     {
        //         SetMoveSpeed(Vector2Int.left);
        //     }
        //     else if (UnityEngine.Input.GetKeyDown(KeyCode.D))
        //     {
        //         SetMoveSpeed(Vector2Int.right);
        //     }
        //
        //     if (UnityEngine.Input.GetKeyDown(KeyCode.D))
        //     {
        //         SetMoveSpeed(Vector2Int.zero);
        //     }
        // }

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

        private void FlipPlayerSprite()
        {
        }
    }
}