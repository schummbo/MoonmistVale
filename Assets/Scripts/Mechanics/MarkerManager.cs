using UnityEngine;
using UnityEngine.Tilemaps;

public class MarkerManager : MonoBehaviour
{
    [SerializeField] private Tilemap markerTilemap;
    [SerializeField] private TileBase markerTile;

    public Vector3Int markedCell;
    private Vector3Int previousMarkedCell;
    public bool IsMarking;

    void Update()
    {
        if (IsMarking)
        {
            markerTilemap.SetTile(previousMarkedCell, null);
            markerTilemap.SetTile(markedCell, markerTile);
            previousMarkedCell = markedCell;
        }
        else
        {
            markerTilemap.SetTile(previousMarkedCell, null);
            markerTilemap.SetTile(markedCell, null);
        }
        
    }
}
