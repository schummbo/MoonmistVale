using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapReadController : MonoBehaviour
{
    [SerializeField] private Tilemap groundTileMap;
    [SerializeField] private List<TileData> TileDatas;
    public CropsManager cropsManager;

    private Dictionary<TileBase, TileData> dataFromTiles;

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

    public List<Vector3Int> GetCellsAroundPosition(Vector2 characterPosition, int selectableCellRadius)
    {
        var gridPosition = GetGridPosition(characterPosition);

        var cells = new List<Vector3Int>();

        // get cells within selectable radius
        for (int rowI = (selectableCellRadius * -1); rowI <= selectableCellRadius; rowI++)
        {
            for (int colI = (selectableCellRadius * -1); colI <= selectableCellRadius; colI++)
            {
                cells.Add(new Vector3Int(gridPosition.x - colI, gridPosition.y - rowI));
            }
        }

        return cells;

    }
}
