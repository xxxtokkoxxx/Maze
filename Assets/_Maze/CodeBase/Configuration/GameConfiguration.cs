using System.Threading.Tasks;
using _Maze.CodeBase.Infrastructure.ResourcesManagement;
using UnityEngine;

namespace _Maze.CodeBase.Configuration
{
    public class GameConfiguration : IGameConfiguration
    {
        private readonly IAssetsLoaderService _assetsLoaderService;

        public GameConfiguration(IAssetsLoaderService assetsLoaderService)
        {
            _assetsLoaderService = assetsLoaderService;
        }

        public int MaxExists { get; private set; }
        public int MinExists { get; private set; }
        public Vector2Int MaxMazeSize { get; private set; }
        public Vector2Int MinMazeSize { get; private set; }

        public async Task LoadConfiguration()
        {
            GameConfigurationSO configuration =
                await _assetsLoaderService.LoadAsset<GameConfigurationSO>(AssetsDataPath.Configuration);

            MaxExists = configuration.MaxMazExists;
            MinExists = configuration.MinMazeExists;
            MaxMazeSize = configuration.MaxMazeSize;
            MinMazeSize = configuration.MinMazeSize;
        }
    }
}