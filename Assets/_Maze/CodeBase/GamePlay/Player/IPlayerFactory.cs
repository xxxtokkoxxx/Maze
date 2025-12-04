using System.Threading.Tasks;
using UnityEngine;

namespace _Maze.CodeBase.GamePlay.Player
{
    public interface IPlayerFactory
    {
        Task LoadPlayerReference();
        PlayerView CreatePlayer(Vector2 position, Transform parent);
        PlayerView GetPlayerView();
        void DestroyPlayerView();
    }
}