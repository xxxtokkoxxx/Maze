using UnityEngine;

namespace _Maze.CodeBase.GamePlay.Camera
{
    public interface ICameraFollowSystem
    {
        void Initialize(Transform target);
        void Disable();
    }
}