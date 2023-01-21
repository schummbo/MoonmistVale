using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MarkerManager : MonoBehaviour
{
    [SerializeField] private Tilemap markerTilemap;
    [SerializeField] private TileBase markerTile;

    public Vector3Int markedCell;
    private Vector3Int previousMarkedCell;

    void Update()
    {
        markerTilemap.SetTile(previousMarkedCell, null);
        markerTilemap.SetTile(markedCell, markerTile);

        previousMarkedCell = markedCell;
        
    }
}
