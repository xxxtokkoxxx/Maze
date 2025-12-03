using UnityEngine;

namespace _Maze.CodeBase.GamePlay.Maze
{
    public interface IMazeFactory
    {
        void DestroyMazeEnvironment();
        GameObject CreateWall(Vector3 position, Quaternion rotation, Transform parent);
    }
}