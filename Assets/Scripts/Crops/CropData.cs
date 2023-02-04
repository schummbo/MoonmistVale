using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Crops
{
    [CreateAssetMenu(menuName = "Data/Crop")]
    public class CropData : ScriptableObject
    {
        public int PhasesToGrow;
        public Item Yield;
        public int Count = 1;

        public List<Sprite> GrowthStages;
        public List<int> GrowthStagePhases;
    }
}
