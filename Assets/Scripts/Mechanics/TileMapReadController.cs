using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Input = UnityEngine.Input;

public class TileMapReadController : MonoBehaviour
{
    [SerializeField] private Tilemap groundTileMap;
    [SerializeField] private List<TileData> TileDatas;

    private Dictionary<TileBase, TileData> dataFromTiles;

    private void Start()
    {
        dataFromTiles = new Dictionary<TileBase, TileData>();

        foreach (var tileData in TileDatas)
        {
            foreach (var tileBase in tileData.Tiles)
            {
                dataFromTiles.TryAdd(tileBase, tileData);
            }
        }
    }

    public Vector3Int GetGridPosition(Vector2 position, bool mousePosition = false)
    {
        var worldPoint = position;

        if (mousePosition)
        {
            worldPoint = Camera.main.ScreenToWorldPoint(position);
        }

        return groundTileMap.WorldToCell(worldPoint);
    }

    public TileBase GetTileBase(Vector3Int cell)
    {
        return groundTileMap.GetTile(cell);
    }

    public TileData GetTileData(TileBase tileBase)
    {
        return dataFromTiles.ContainsKey(tileBase) ? dataFromTiles[tileBase] : null;
    }
}
