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
        VisualizeMap();
    }

    private void VisualizeMap()
    {
        foreach (var crop in cropsContainer.Crops)
        {
            VisualizeTile(crop);
        }
    }

    private void OnDestroy()
    {
        foreach (var t in cropsContainer.Crops)
        {
            t.spriteRenderer = null;
        }
    }

    public void VisualizeTile(Crop crop)
    {
        cropsTilemap.SetTile(crop.position, crop.cropData != null ? seededTile : plowedTile);

        if (crop.spriteRenderer == null)
        {
            var go = Instantiate(cropSpritePrefab, transform);
            go.transform.position = cropsTilemap.CellToWorld(crop.position);
            crop.spriteRenderer = go.GetComponent<SpriteRenderer>();
        }

        crop.Show();
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
                cropsTilemap.SetTile(crop.position, plowedTile);
            }

            if (crop.IsDead())
            {
                crop.Harvest();
                cropsTilemap.SetTile(crop.position, plowedTile);
            }
        }
    }

    public bool Plow(Vector3Int position)
    {
        if (cropsContainer.IsPlowed(position))
        {
            return false;
        }

        var crop = new Crop(position);

        cropsContainer.Plow(crop);
        VisualizeTile(crop);

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
            crop.cropData.Yield,
            crop.cropData.Count);

        crop.Harvest();

        VisualizeTile(crop);

        return true;
    }
}
