using System;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Crops
{
    [Serializable]
    public class Crop
    {
        private readonly SpriteRenderer spriteRenderer;
        private int growTimer;
        private int currentGrowthStageIndex;
        private float damage;

        public CropData CropData { get; set; }
        public Vector3Int Position { get; }

        public Crop(Vector3Int position, SpriteRenderer spriteRenderer)
        {
            this.Position = position;
            this.spriteRenderer = spriteRenderer;
        }

        public void Grow()
        {
            growTimer++;

            if (growTimer >= this.CropData.GrowthStages[currentGrowthStageIndex].Phase)
            {
                this.spriteRenderer.sprite = this.CropData.GrowthStages[currentGrowthStageIndex].AppearanceAtStage;
                this.spriteRenderer.gameObject.SetActive(true);

                currentGrowthStageIndex++;
            }
        }

        public bool IsGrowing()
        {
            return growTimer >= this.CropData.GrowthStages.First().Phase;
        }

        public bool IsDead()
        {
            return damage >= 1;
        }

        public void Wither()
        {
            damage += .02f;
        }

        public bool IsGrown()
        {
            if (CropData == null) return false;

            return growTimer >= CropData.PhasesToGrow;
        }

        public void Harvest()
        {
            spriteRenderer.gameObject.SetActive(false);
            CropData = null;
            growTimer = 0;
            currentGrowthStageIndex = 0;
            damage = 0;
            
        }
    }
}
