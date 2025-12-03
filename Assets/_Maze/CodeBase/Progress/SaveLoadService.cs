using _Maze.CodeBase.Data;
using Newtonsoft.Json;
using UnityEngine;

namespace _Maze.CodeBase.Progress
{
    public class SaveLoadService : ISaveLoadService
    {
        private const string SavedGame = "SavedGame";

        public void SaveGame(GameProgressData gameProgressData)
        {
            string data = JsonConvert.SerializeObject(gameProgressData);
            PlayerPrefs.SetString(SavedGame, data);
        }

        public GameProgressData LoadGame()
        {
            string dataJson = PlayerPrefs.GetString(SavedGame);
            GameProgressData data = JsonConvert.DeserializeObject<GameProgressData>(dataJson);

            return data;
        }

        public bool SaveExists()
        {
            return PlayerPrefs.HasKey(SavedGame);
        }
    }
}