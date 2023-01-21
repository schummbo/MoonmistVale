using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Crops;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CropsManager : MonoBehaviour
{
    [SerializeField] private TileBase plowedTile;
    [SerializeField] private TileBase seededTile;
    [SerializeField] private Tilemap cropsTilemap;

    private Dictionary<Vector2Int, Crop> crops;

    void Start()
    {
        crops = new Dictionary<Vector2Int, Crop>();
    }

    public void Plow(Vector3Int position)
    {
        if (crops.ContainsKey((Vector2Int)position))
        {
            return;
        }

        PlowTile(position);
    }

    public void Seed(Vector3Int position)
    {
        cropsTilemap.SetTile(position, seededTile);
    }

    public bool Check(Vector3Int position)
    {
        return crops.ContainsKey((Vector2Int)position);
    }

    private void PlowTile(Vector3Int position)
    {
        Crop crop = new Crop();

        crops.Add((Vector2Int)position, crop);

        cropsTilemap.SetTile(position, plowedTile);
    }
}
