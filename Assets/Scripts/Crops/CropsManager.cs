using Assets.Scripts.Crops;
using UnityEngine;

public class CropsManager : MonoBehaviour
{
    [HideInInspector]
    public TilemapCropsManager TilemapCropsManager;

    public bool PickUp(Vector3Int position)
    {
        if (TilemapCropsManager == null)
        {
            return false;
        }

        return TilemapCropsManager.PickUp(position);
    }

    public bool CheckIfPlowed(Vector3Int position)
    {
        if (TilemapCropsManager == null)
        {
            return false;
        }

        return TilemapCropsManager.CheckIfPlowed(position);
    }

    public bool PlantCrop(Vector3Int position, CropData cropToSeed)
    {
        if (TilemapCropsManager == null)
        {
            return false; 
        }

        return TilemapCropsManager.PlantCrop(position, cropToSeed);
    }

    public bool Plow(Vector3Int position)
    {
        if (TilemapCropsManager == null)
        {
            return false;
        }

        return TilemapCropsManager.Plow(position);
    }
}
