using System.Threading.Tasks;
using UnityEngine;

namespace _Maze.CodeBase.GamePlay.Player
{
    public interface IPlayerFactory
    {
        Task LoadPlayerReference();
        IPlayer CreatePlayer(Vector2 position, Transform parent);
        IPlayer GetPlayer();
        void DestroyPlayerView();
    }
}