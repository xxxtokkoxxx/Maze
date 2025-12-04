using UnityEngine;

namespace _Maze.CodeBase.GamePlay.Camera
{
    public class CameraSizeAdjuster : MonoBehaviour
    {
        [SerializeField] private UnityEngine.Camera _camera;
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