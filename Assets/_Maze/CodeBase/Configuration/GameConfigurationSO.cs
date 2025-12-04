using UnityEngine;

namespace _Maze.CodeBase.Configuration
{
    [CreateAssetMenu(fileName = "GameConfiguration", menuName = "ScriptableObjects/Configuration", order = 1)]
    public class GameConfigurationSO : ScriptableObject
    {
        public Vector2Int MaxMazeSize = new Vector2Int(100,100);
        public Vector2Int MinMazeSize = new Vector2Int(10,10);
        public int MaxMazExists = 5;
        public int MinMazeExists = 1;
    }
}