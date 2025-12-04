using UnityEngine;

namespace _Maze.CodeBase.Animations
{
    public class PlayerAnimator : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        public void PlayMove(bool move)
        {
            _animator.SetBool(PlayerAnimatorHashes.Move, move);
        }

        public void PlayJump()
        {
            _animator.SetTrigger(PlayerAnimatorHashes.Jump);
        }
    }

    public class PlayerAnimatorHashes
    {
        private const string MoveCondition = "Move";
        private const string JumpCondition = "Jump";

        public static readonly int Move = Animator.StringToHash(MoveCondition);
        public static readonly int Jump = Animator.StringToHash(JumpCondition);
    }
}