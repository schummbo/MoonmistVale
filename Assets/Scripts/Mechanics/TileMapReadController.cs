using System.Collections.Generic;
using Assets.Scripts.PubSub;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapReadController : MonoBehaviour
{
    private Tilemap groundTileMap;
    [SerializeField] private List<TileData> TileDatas;
    public CropsManager cropsManager;
    [SerializeField] private PubSubEvents pubSubEvents;

    void Awake()
    {
        SetGroundTilemap();
    }

    void OnEnable()
    {
        pubSubEvents.OnScenePreChange += RemoveGroundTilemap;
        pubSubEvents.OnScenePostChange += SetGroundTilemap;
    }

    void OnDisable()
    {
        pubSubEvents.OnScenePreChange -= RemoveGroundTilemap;
        pubSubEvents.OnScenePostChange -= SetGroundTilemap;
    }

    private void RemoveGroundTilemap()
    {
        groundTileMap = null;
    }

    private void SetGroundTilemap()
    {
        var groundTileMapObject = GameObject.Find("Ground");

        if (groundTileMapObject != null)
        {
            groundTileMap = groundTileMapObject.GetComponent<Tilemap>();
        }
    }

    public Vector3Int GetGridPosition(Vector2 position, bool mousePosition = false)
    {
        if (groundTileMap == null)
        {
            return Vector3Int.zero;
        }

        var worldPoint = position;

        if (mousePosition)
        {
            worldPoint = Camera.main.ScreenToWorldPoint(position);
        }

        return groundTileMap.WorldToCell(worldPoint);
    }

    public TileBase GetTileBase(Vector3Int cell)
    {
        if (groundTileMap == null)
        {
            return null;
        }

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
