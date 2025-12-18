using _Maze.CodeBase.GamePlay.Player;
using UnityEngine;

namespace _Maze.CodeBase.GamePlay.EnvironmentalHazards
{
    public class DamageSource : MonoBehaviour
    {
        [SerializeField] private int _damage = 1;

        private void OnTriggerEnter2D(Collider2D other)
        {
            bool notNull = other.gameObject.TryGetComponent(out IPlayer player);

            if (notNull)
            {
                player.DoDamage(_damage);
            }
        }
    }
}