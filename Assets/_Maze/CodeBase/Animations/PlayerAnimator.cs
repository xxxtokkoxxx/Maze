using System.Linq;
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

        public void PlayJump(float duration)
        {
            float baseLength = _animator.runtimeAnimatorController
                .animationClips.First(c => c.name == "Jump").length;

            float speed = baseLength / duration;
            _animator.SetFloat(PlayerAnimatorHashes.Speed, speed);

            _animator.SetTrigger(PlayerAnimatorHashes.Jump);
        }
    }

    public class PlayerAnimatorHashes
    {
        private const string MoveCondition = "Move";
        private const string JumpCondition = "Jump";
        private const string SpeedCondition = "Speed";

        public static readonly int Move = Animator.StringToHash(MoveCondition);
        public static readonly int Jump = Animator.StringToHash(JumpCondition);
        public static readonly int Speed = Animator.StringToHash(SpeedCondition);
    }
}