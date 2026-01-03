using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace _Maze.CodeBase.GamePlay.Environment.Behaviours
{
    [ExecuteInEditMode]
    public class MoverAccordingToRoute : MonoBehaviour, ILevelElement
    {
        [SerializeField] private List<Vector2> _route;
        [SerializeField] private List<Vector2> _initialRoute;
        [SerializeField] private float _movementDuration = 1;
        [SerializeField] private bool _enableReverse;
        
        private Vector2 _initialPosition;

        private void Start()
        {
            _initialRoute = _route;
            transform.localPosition = _initialRoute.Any() 
                ? _initialRoute.First()
                : transform.localPosition;
            
            StartCoroutine(Move());
        }

        public void SaveState()
        {
            _initialPosition = transform.localPosition;
            _initialRoute = _route;
        }

        public void RestoreState()
        {
            transform.localPosition = _initialPosition;
            _route = _initialRoute;
            StopAllCoroutines();
            transform.DOKill();
            StartCoroutine(Move());
        }
        
        private IEnumerator Move()
        {
            if (_route == null || _route.Count <= 0)
            {
                yield break;
            }

            int index = 0;

            while (true)
            {
                yield return MoveNextPoint(_route[index]);
                index++;
                if (index >= _route.Count)
                {
                    if (_enableReverse)
                    {
                        _route.Reverse();
                    }
                    
                    index = 0;
                }
            }
        }

        private IEnumerator MoveNextPoint(Vector2 position)
        {
            transform.DOLocalMove(position, _movementDuration);
            yield return new WaitForSeconds(_movementDuration);
        }

#if UNITY_EDITOR

        [Button]
        private void AddPoint()
        {
            _route.Add(transform.localPosition);
        }

        [Button]
        public void MoveLeft()
        {
            transform.localPosition += Vector3.left;
        }
        
        [Button]
        public void MoveRight()
        {
            transform.localPosition += Vector3.right;
        }

        [Button]
        public void MoveUp()
        {
            transform.localPosition += Vector3.up;
        }

        [Button]
        public void MoveDown()
        {
            transform.localPosition += Vector3.down;
        }

        
        private void OnDrawGizmosSelected()
        {
            if (_route == null || _route.Count <= 0)
            {
                return;
            }

            Vector2 previousPos = _route[0];

            foreach (Vector2 point in _route)
            {
                Handles.color = Color.blue;
                Handles.DrawLine(previousPos, point, 5);

                previousPos = point;
            }
        }
#endif
    }
}