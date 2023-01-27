using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Crops;
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
            crop.Grow();

            if (crop.IsGrown())
            {
                Debug.Log("I'm done growing");
                crop.Clear();
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
        Crop crop = new Crop();

        cropTiles.Add((Vector2Int)position, crop);

        GameObject go = Instantiate(cropSpritePrefab, cropsTilemap.CellToWorld(position), Quaternion.identity);
        go.SetActive(false);
        crop.spriteRenderer = go.GetComponent<SpriteRenderer>();

        cropsTilemap.SetTile(position, plowedTile);
    }
}
