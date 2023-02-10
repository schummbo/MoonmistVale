using System;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Crops
{
    [Serializable]
    public class Crop
    {
        public SpriteRenderer spriteRenderer;
        public int growTimer;
        public int currentGrowthStageIndex = -1;
        public float damage;

        public CropData cropData;
        public Vector3Int position;

        public Crop(Vector3Int position)
        {
            this.position = position;
        }

        public void Show()
        {
            if (this.cropData != null)
            {
                this.spriteRenderer.sprite = this.cropData.GrowthStages[currentGrowthStageIndex].AppearanceAtStage;
            }

            this.spriteRenderer.gameObject.SetActive(this.IsGrowing());
        }

        public void Grow()
        {
            growTimer++;

            if (currentGrowthStageIndex + 1 < cropData.GrowthStages.Count && 
                 growTimer >= cropData.GrowthStages[currentGrowthStageIndex + 1].Phase)
            {
                currentGrowthStageIndex++;
                Show();
            }
        }

        public bool IsGrowing()
        {
            return this.cropData != null && growTimer >= this.cropData.GrowthStages.First().Phase;
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
            if (cropData == null) return false;

            return growTimer >= cropData.PhasesToGrow;
        }

        public void Harvest()
        {
            spriteRenderer.gameObject.SetActive(false);
            cropData = null;
            growTimer = 0;
            currentGrowthStageIndex = 0;
            damage = 0;
        }
    }
}
