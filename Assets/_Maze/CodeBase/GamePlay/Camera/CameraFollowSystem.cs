using _Maze.CodeBase.Infrastructure;
using UnityEngine;
using VContainer.Unity;

namespace _Maze.CodeBase.GamePlay.Camera
{
    public class CameraFollowSystem : ITickable, ICameraFollowSystem
    {
        private readonly IMonoBehavioursProvider _monoBehavioursProvider;
        private bool _isEnabled;
        private Transform _cameraTransform;
        private Transform _target;

        public CameraFollowSystem(IMonoBehavioursProvider monoBehavioursProvider)
        {
            _monoBehavioursProvider = monoBehavioursProvider;
        }

        public void Initialize(Transform target)
        {
            _target = target;
            _isEnabled = true;
            _cameraTransform = _monoBehavioursProvider.CameraTransform;
        }

        public void Disable()
        {
            _isEnabled = false;
        }

        public void Tick()
        {
            if (_isEnabled)
            {
                Vector2 targetPos = Vector2.Lerp(_cameraTransform.position, _target.position, Time.deltaTime * 10f);
                _cameraTransform.position = new Vector3(targetPos.x, targetPos.y, _cameraTransform.position.z);
            }
        }
    }
}