using Assets.Scripts.Crops;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.PubSub;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CropsManager : TimeBasedBehaviorBase
{
    [SerializeField] private TileBase plowedTile;
    [SerializeField] private TileBase seededTile;
    private Tilemap cropsTilemap;
    [SerializeField] private GameObject cropSpritePrefab;

    private Dictionary<Vector2Int, Crop> cropTiles;

    void Awake()
    {
        SetPlowedTilemap();
    }

    new void OnEnable()
    {
        pubSubEvents.OnScenePostChange += SetPlowedTilemap;
        base.OnEnable();
    }

    new void OnDisable()
    {
        pubSubEvents.OnScenePostChange -= SetPlowedTilemap;
        base.OnDisable();

    }

    private void SetPlowedTilemap()
    {
        var groundTileMapObject = GameObject.Find("Plowed");

        if (groundTileMapObject != null)
        {
            cropsTilemap = groundTileMapObject.GetComponent<Tilemap>();
        }
    }

    void Start()
    {
        cropTiles = new Dictionary<Vector2Int, Crop>();
    }

    protected override void HandlePhaseStarted(int phase)
    {
        foreach (Crop crop in cropTiles.Values.Where(tile => tile.CropData != null))
        {
            if (!crop.IsGrown())
            {
                crop.Grow();
            }

            crop.Wither();

            if (crop.IsGrowing())
            {
                cropsTilemap.SetTile(crop.Position, plowedTile);
            }

            if (crop.IsDead())
            {
                crop.Harvest();
                cropsTilemap.SetTile(crop.Position, plowedTile);
            }
        }
    }

    public void Plow(Vector3Int position)
    {
        if (cropTiles.ContainsKey((Vector2Int)position))
        {
            return;
        }

        PlowTile(position);
    }

    public void PlantCrop(Vector3Int position, CropData toSeed)
    {
        if (cropTiles.TryGetValue((Vector2Int)position, out Crop item))
        {
            if (item.CropData == null)
            {
                cropsTilemap.SetTile(position, seededTile);
                cropTiles[(Vector2Int)position].CropData = toSeed;
            }
        }
    }

    public bool CheckIfPlowed(Vector3Int position)
    {
        return cropTiles.ContainsKey((Vector2Int)position);
    }

    private void PlowTile(Vector3Int position)
    {
        var go = Instantiate(cropSpritePrefab, cropsTilemap.CellToWorld(position), Quaternion.identity);
        go.SetActive(false);

        var crop = new Crop(position, go.GetComponent<SpriteRenderer>());

        cropTiles.Add((Vector2Int)position, crop);

        cropsTilemap.SetTile(position, plowedTile);
    }

    public void PickUp(Vector3Int tileMapPosition)
    {
        var position = (Vector2Int)tileMapPosition;

        if (!cropTiles.TryGetValue(position, out Crop crop))
        {
            return;
        }

        if (crop.IsGrown())
        {
            ItemSpawnManager.Instance.SpawnItem(
                cropsTilemap.CellToWorld(tileMapPosition),
                crop.CropData.Yield,
                crop.CropData.Count);

            crop.Harvest();
            cropsTilemap.SetTile(tileMapPosition, plowedTile);
        }
    }
}
