using Sirenix.OdinInspector;
using UnityEngine;

namespace _Maze.CodeBase.Animations
{
    public class CoinAnimator : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        [Button]
        public void PlayPickCoin(bool isPicked)
        {
            _animator.SetBool(CoinAnimatorHashes.PickCoin, isPicked);
        }
    }

    public class CoinAnimatorHashes
    {
        private const string PickCoinCondition = "PickCoin";

        public static readonly int PickCoin = Animator.StringToHash(PickCoinCondition);
    }
}