using _Maze.CodeBase.GamePlay.Maze;
using UnityEngine;

namespace _Maze.CodeBase.GamePlay.Player
{
    public interface IPlayer
    {
        void DoDamage(int damage);
        void ResetPosition();
        GameObject View { get; }
        void PlayJumpAnimation(float duration);
        SpriteRenderer Visuals { get; }
        void SetVisualsDirection(Direction direction);
    }
}