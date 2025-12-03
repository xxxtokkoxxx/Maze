using System.Threading.Tasks;
using UnityEngine;

namespace _Maze.CodeBase.GamePlay.Player
{
    public interface IPlayerFactory
    {
        Task LoadPlayerReference();
        GameObject CreatePlayer(Vector2 position, Transform parent);
        GameObject GetPlayerView();
        void DestroyPlayerView();
        void ReleaseResources();
    }
}