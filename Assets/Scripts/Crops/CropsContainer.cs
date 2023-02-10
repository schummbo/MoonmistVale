using Assets.Scripts.Crops;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Crops Container")]
public class CropsContainer : ScriptableObject
{
    public List<Crop> Crops;

    public IEnumerable<Crop> GetPlantedCrops()
    {
        return Crops?.Where(tile => tile.cropData != null) ?? Enumerable.Empty<Crop>();
    }

    public bool IsPlowed(Vector3Int position)
    {
        return Crops?.Find(crop => crop.position == position) != null;
    }

    public void Plow(Crop crop)
    {
        if (Crops == null)
        {
            return;
        }

        if (!IsPlowed(crop.position))
        {
            Crops.Add(crop);
        }
    }

    public Crop GetCrop(Vector3Int position)
    {
        return Crops?.Find(crop => crop.position == position);
    }

    public bool Seed(Vector3Int position, CropData cropData)
    {
        var crop = GetCrop(position);

        if (crop == null || crop.cropData != null)
        {
            return false;
        }
        
        crop.cropData = cropData;

        return true;
    }
}
