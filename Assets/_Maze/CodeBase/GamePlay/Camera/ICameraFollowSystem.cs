using UnityEngine;

namespace _Maze.CodeBase.GamePlay.Camera
{
    public interface ICameraFollowSystem
    {
        void SetTarget(Transform target);
        void Disable();
    }
}