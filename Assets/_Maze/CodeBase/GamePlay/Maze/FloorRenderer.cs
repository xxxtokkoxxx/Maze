using UnityEngine;
using UnityEngine.Tilemaps;

namespace _Maze.CodeBase.GamePlay.Maze
{
    public class FloorRenderer : MonoBehaviour
    {
        [SerializeField] private Tilemap _floorTilemap;
        [SerializeField] private TileBase _floorTile;

        public void GenerateFloor(int width, int height)
        {
            for (int x = 0; x < width + width; x++)
            {
                for (int y = 0; y < height + height; y++)
                {
                    _floorTilemap.SetTile(new Vector3Int(x, y, 0), _floorTile);
                }
            }
        }

        public void ClearAllTiles()
        {
            _floorTilemap.ClearAllTiles();
        }
    }
}