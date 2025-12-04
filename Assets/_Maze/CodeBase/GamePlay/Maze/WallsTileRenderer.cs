using UnityEngine;
using UnityEngine.Tilemaps;

namespace _Maze.CodeBase.GamePlay.Maze
{
    public class WallsTileRenderer : MonoBehaviour
    {
        [SerializeField] private Tilemap _horizontalTileMap;
        [SerializeField] private Tilemap _verticalTileMap;
        [SerializeField] private TileBase _wallTile;

        public void GenerateTile(int x, int y, bool isHorizontal)
        {
            Vector3Int tilePos = new Vector3Int(x, y, 0);

            if (isHorizontal)
            {
                _horizontalTileMap.SetTile(tilePos, _wallTile);
                _horizontalTileMap.SetTransformMatrix(
                    tilePos,
                    Matrix4x4.Translate(new Vector3(0f, -0.5f, 0f))
                );
            }
            else
            {
                _verticalTileMap.SetTile(tilePos, _wallTile);
                _verticalTileMap.SetTransformMatrix(
                    tilePos,
                    Matrix4x4.Translate(new Vector3(-0.5f, 0f, 0f))
                );
            }
        }

        public void ClearAllTiles()
        {
            _horizontalTileMap.ClearAllTiles();
            _verticalTileMap.ClearAllTiles();
        }
    }
}