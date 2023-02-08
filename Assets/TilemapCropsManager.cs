using Assets.Scripts.Crops;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Tilemap))]
public class TilemapCropsManager : TimeBasedBehaviorBase
{
    [SerializeField] private CropsContainer cropsContainer;
    [SerializeField] private TileBase plowedTile;
    [SerializeField] private TileBase seededTile;
    [SerializeField] private GameObject cropSpritePrefab;

    private Tilemap cropsTilemap;

    void Awake()
    {
        cropsTilemap = GetComponent<Tilemap>();
    }

    void Start()
    {
        GameManager.Instance.GetComponent<CropsManager>().TilemapCropsManager = this;
    }

    protected override void HandlePhaseStarted(int phase)
    {
        foreach (Crop crop in cropsContainer.GetPlantedCrops())
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

    public bool Plow(Vector3Int position)
    {
        if (cropsContainer.IsPlowed(position))
        {
            return false;
        }

        var go = Instantiate(cropSpritePrefab, cropsTilemap.CellToWorld(position), Quaternion.identity);
        go.SetActive(false);

        var crop = new Crop(position, go.GetComponent<SpriteRenderer>());

        cropsContainer.Plow(crop);

        cropsTilemap.SetTile(position, plowedTile);

        return true;
    }

    public bool PlantCrop(Vector3Int position, CropData toSeed)
    {
        if (cropsContainer.Seed(position, toSeed))
        {
            cropsTilemap.SetTile(position, seededTile);
            return true;
        }

        return false;
    }

    public bool CheckIfPlowed(Vector3Int position)
    {
        return cropsContainer.IsPlowed(position);
    }

    public bool PickUp(Vector3Int tileMapPosition)
    {
        var crop = cropsContainer.GetCrop(tileMapPosition);

        if (crop == null)
        {
            return false;
        }

        if (!crop.IsGrown())
        {
            return false;
        }

        ItemSpawnManager.Instance.SpawnItem(
            cropsTilemap.CellToWorld(tileMapPosition),
            crop.CropData.Yield,
            crop.CropData.Count);

        crop.Harvest();

        cropsTilemap.SetTile(tileMapPosition, plowedTile);

        return true;
    }
}
