using Assets.Scripts.Crops;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CropsManager : TimeBasedBehaviorBase
{
    [SerializeField] private TileBase plowedTile;
    [SerializeField] private TileBase seededTile;
    [SerializeField] private Tilemap cropsTilemap;
    [SerializeField] private GameObject cropSpritePrefab;

    private Dictionary<Vector2Int, Crop> cropTiles;

    new void Start()
    {
        cropTiles = new Dictionary<Vector2Int, Crop>();
        base.Start();
    }

    protected override void HandlePhaseStarted(int phase)
    {
        foreach (Crop crop in cropTiles.Values.Where(tile => tile.CropData != null))
        {
            if (!crop.IsGrown())
            {
                crop.Grow();
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
        cropsTilemap.SetTile(position, seededTile);
        cropTiles[(Vector2Int)position].CropData = toSeed;
    }

    public bool CheckIfPlowed(Vector3Int position)
    {
        return cropTiles.ContainsKey((Vector2Int)position);
    }

    private void PlowTile(Vector3Int position)
    {
        var go = Instantiate(cropSpritePrefab, cropsTilemap.CellToWorld(position), Quaternion.identity);
        go.SetActive(false);

        var crop = new Crop(go.GetComponent<SpriteRenderer>());

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
        }
    }
}
