using UnityEngine;

namespace Assets.Scripts.Crops
{
    public class Crop
    {
        public CropData CropData;
        private int growTimer;
        public SpriteRenderer spriteRenderer;
        private int currentGrowthStageIndex = 0;

        public void Grow()
        {
            growTimer++;

            if (growTimer >= this.CropData.GrowthStagePhases[currentGrowthStageIndex])
            {
                this.spriteRenderer.sprite = this.CropData.GrowthStages[currentGrowthStageIndex];
                this.spriteRenderer.gameObject.SetActive(true);

                currentGrowthStageIndex++;
            }
        }

        public bool IsGrown()
        {
            return growTimer >= CropData.PhasesToGrow;
        }

        public void Clear()
        {
            CropData = null;
        }
    }
}
