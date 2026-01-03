using DG.Tweening;
using UnityEngine;

namespace _Maze.CodeBase.GamePlay.Environment.Behaviours
{
    public class LocalRotationAnimation : MonoBehaviour
    {
        [SerializeField] private float _rotationSpeed = 0.5f;

        private void Start()
        {
            transform
                .DOLocalRotate(new Vector3(0f, 0f, 360f), _rotationSpeed, RotateMode.LocalAxisAdd)
                .SetEase(Ease.Linear)
                .SetLoops(-1);
        }
    }
}