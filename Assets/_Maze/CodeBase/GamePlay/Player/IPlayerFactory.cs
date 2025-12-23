using System.Threading.Tasks;
using UnityEngine;

namespace _Maze.CodeBase.GamePlay.Player
{
    public interface IPlayerFactory
    {
        void SetPlayerReference();
        IPlayer CreatePlayer(Vector2 position, Transform parent);
        IPlayer GetPlayer();
        void DestroyPlayerView();
    }
}