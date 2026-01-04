using _Maze.CodeBase.GamePlay.Maze;
using UnityEngine;

namespace _Maze.CodeBase.GamePlay.Player
{
    public interface IPlayer
    {
        void DoDamage(int damage);
        void ResetPosition();
        GameObject View { get; }
        Rigidbody2D Rigidbody { get; }
        SpriteRenderer Visuals { get; }
        void SetVisualsDirection(Direction direction);
        void PlayJumpAnimation(float duration);
        void PlayLevelCompletionAnimation();
    }
}