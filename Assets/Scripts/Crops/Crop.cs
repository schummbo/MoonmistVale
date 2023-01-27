using UnityEngine;

namespace Assets.Scripts.Crops
{
    public class Crop
    {
        private readonly SpriteRenderer spriteRenderer;
        private int growTimer;
        private int currentGrowthStageIndex;

        public Crop(SpriteRenderer spriteRenderer)
        {
            this.spriteRenderer = spriteRenderer;
        }

        public CropData CropData { get; set; }

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
            if (CropData == null) return false;

            return growTimer >= CropData.PhasesToGrow;
        }

        public void Harvest()
        {
            spriteRenderer.sprite = CropData.HarvestedSprite;
            CropData = null;
            growTimer = 0;
            currentGrowthStageIndex = 0;

        }
    }
}
