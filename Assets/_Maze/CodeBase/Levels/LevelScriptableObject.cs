using UnityEngine;
using UnityEngine.AddressableAssets;

namespace _Maze.CodeBase.Levels
{
    [CreateAssetMenu(fileName = "Level_", menuName = "ScriptableObjects/Levels", order = 1)]
    public class LevelScriptableObject : ScriptableObject
    {
        public string Id;
        public string Category;
        public AssetReference SceneReference;
    }
}