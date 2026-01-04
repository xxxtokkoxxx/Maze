using System.Collections;
using System.Collections.Generic;
using _Maze.CodeBase.Utilities;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Maze.CodeBase.Levels
{
    [CreateAssetMenu(fileName = "Level_", menuName = "ScriptableObjects/Levels", order = 1)]
    public class LevelScriptableObject : ScriptableObject
    {
        public string Id;
        [ValueDropdown(nameof(Levels))] [OnValueChanged(nameof(UpdateCategory))] public string Category;
        [ValueDropdown(nameof(GetLevelReferences),DropdownTitle = "Select level")] public string LevelAddress;

        public static IEnumerable Levels = new List<string>()
        {
            LevelCategories.Chest,
        };

        public void UpdateCategory()
        {
            LevelAddress = LevelCategories.Chest;
        }

        public IEnumerable<string> GetLevelReferences()
        {
            return UnityEditorAssetLoader.GetResourceLocations<string>(Category);
        }
    }

    public class LevelCategories
    {
        public const string Chest = "Chest";
    }
}