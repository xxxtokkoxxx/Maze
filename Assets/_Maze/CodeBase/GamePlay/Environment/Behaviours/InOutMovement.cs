using System.Collections;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Maze.CodeBase.GamePlay.Environment.Behaviours
{
    public class InOutMovement : MonoBehaviour, ILevelElement
    {
        [SerializeField] private float _moveAmount = 1;
        [SerializeField] private float _cooldown = 1;
        [SerializeField] private InOutState _initialState;
        [SerializeField] private float _movementSpeed = 0.3f;
        [SerializeField] private Transform _targetTransform;

        private InOutState _currentState;
        private float _cooldownCounter;
        private Vector2 _initialPosition;

        private void Start()
        {
            if (_targetTransform == null)
            {
                _targetTransform = transform;
            }

            _initialPosition = _targetTransform.localPosition;
            SetState(_initialState);
            StartCoroutine(PlayAnimation());
            _cooldownCounter = _cooldown;
        }

        public void SaveState()
        {
            _initialPosition = _targetTransform.localPosition;
        }

        public void RestoreState()
        {
            _targetTransform.localPosition = _initialPosition;
            SetState(_initialState);
            _cooldownCounter = _cooldown;
        }

        [Button]
        private IEnumerator PlayAnimation()
        {
            while (true)
            {
                _cooldownCounter -= Time.deltaTime;

                if (_cooldownCounter <= 0)
                {
                    InOutState nextState = _currentState == InOutState.In
                        ? InOutState.Out
                        : InOutState.In;

                    SetState(nextState);
                    _cooldownCounter = _cooldown;
                }

                yield return null;
            }
        }

        [Button]
        private void SetState(InOutState state)
        {
            if (_currentState == state)
                return;

            _currentState = state;
            if (state == InOutState.Out)
            {
                _targetTransform.DOLocalMoveY(_initialPosition.y + _moveAmount, _movementSpeed).SetEase(Ease.InQuad);
            }
            else
            {
                _targetTransform.DOLocalMove(_initialPosition, _movementSpeed).SetEase(Ease.OutQuad);
            }
        }
    }
}