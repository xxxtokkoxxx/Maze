using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Maze.CodeBase.Animations
{
    public class PlayerAnimator : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        [Button]
        public void PlayMove(bool move)
        {
            _animator.SetBool(PlayerAnimatorHashes.Move, move);
        }

        [Button]
        public void PlayIdle()
        {
            _animator.SetTrigger(PlayerAnimatorHashes.Idle);
        }

        [Button]
        public void PlayJump(float duration)
        {
            float baseLength = _animator.runtimeAnimatorController
                .animationClips.First(c => c.name == "Jump").length;

            float speed = baseLength / duration;
            _animator.SetFloat(PlayerAnimatorHashes.Speed, speed);

            _animator.SetTrigger(PlayerAnimatorHashes.Jump);
        }

        [Button]
        public void PlayDeath()
        {
            _animator.SetTrigger(PlayerAnimatorHashes.Death);
            Debug.Log("play ");
        }

        [Button]
        public void PlayDisappearance()
        {
            _animator.SetTrigger(PlayerAnimatorHashes.Disappear);
        }
    }

    public class PlayerAnimatorHashes
    {
        private const string MoveCondition = "Move";
        private const string JumpCondition = "Jump";
        private const string IdleCondition = "Idle";
        private const string SpeedCondition = "Speed";
        private const string DeathCondition = "Death";
        private const string DisappearCondition = "Disappear";

        public static readonly int Move = Animator.StringToHash(MoveCondition);
        public static readonly int Idle = Animator.StringToHash(IdleCondition);
        public static readonly int Jump = Animator.StringToHash(JumpCondition);
        public static readonly int Speed = Animator.StringToHash(SpeedCondition);
        public static readonly int Death = Animator.StringToHash(DeathCondition);
        public static readonly int Disappear = Animator.StringToHash(DisappearCondition);
    }
}