using UnityEngine;

namespace _Maze.CodeBase.Infrastructure
{
    public class CameraSizeAdjuster : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private float _targetWorldWidth = 20f;

        private void Start()
        {
            ApplyCameraSize();
        }

        private void ApplyCameraSize()
        {
            float aspect = (float)Screen.width / Screen.height;
            _camera.orthographicSize = _targetWorldWidth / 2f / aspect;
        }
    }
}