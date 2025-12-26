using Newtonsoft.Json;
using UnityEngine;

namespace _Maze.CodeBase.Progress
{
    public class SaveLoadService : ISaveLoadService
    {
        private const string SavedGame = "SavedGame";

        public void SaveGame()
        {
            // string data = JsonConvert.SerializeObject(gameProgressData);
            // PlayerPrefs.SetString(SavedGame, data);
        }

        public void LoadGame()
        {
            // string dataJson = PlayerPrefs.GetString(SavedGame);
            // GameProgressData data = JsonConvert.DeserializeObject<GameProgressData>(dataJson);

            // return data;
        }

        public bool SaveExists()
        {
            return PlayerPrefs.HasKey(SavedGame);
        }
    }
}