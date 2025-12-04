using System.Threading.Tasks;
using UnityEngine;

namespace _Maze.CodeBase.GamePlay.Maze
{
    public interface IMazeFactory
    {
        Task LoadReferences();
        GameObject CreateWall(Vector2 position, Quaternion rotation, Transform parent);
        void DestroyMazeEnvironment();
        FloorRenderer GenerateFloor(int width, int height, Vector2 floorPosition, Transform parent);
    }
}