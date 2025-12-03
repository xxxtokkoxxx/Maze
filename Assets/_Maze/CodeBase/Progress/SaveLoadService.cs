using _Maze.CodeBase.Data;
using UnityEngine;

namespace _Maze.CodeBase.Progress
{
    public class SaveLoadService : ISaveLoadService
    {
        public const string SavedGame = "SavedGame";

        public void SaveGame(GameProgressData gameProgressData)
        {
            //TODO:serialize newtonsoft json
            PlayerPrefs.SetString(SavedGame, SavedGame);
        }

        public GameProgressData LoadGame()
        {
            //TODO:serialize newtonsoft json
            string dataJson  = PlayerPrefs.GetString(SavedGame);
            return new GameProgressData(1, null, null);
        }

        public bool SaveExists()
        {
            return PlayerPrefs.HasKey(SavedGame);
        }
    }
}