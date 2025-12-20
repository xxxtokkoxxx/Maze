using UnityEngine;

namespace _Maze.CodeBase.GamePlay.Exit
{
    public class ExitAnimator : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        public void Open()
        {
            _animator.SetBool(ExitAnimatorHashes.Open, true);
        }

        public void Close()
        {
            _animator.SetBool(ExitAnimatorHashes.Open, false);
        }
    }

    public class ExitAnimatorHashes
    {
        private const string OpenCondition = "Open";

        public static readonly int Open = Animator.StringToHash(OpenCondition);
    }
}