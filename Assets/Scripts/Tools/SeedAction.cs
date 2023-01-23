using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Tools
{
    [CreateAssetMenu(menuName = "Data/Tool Actions/Seed Action")]

    public class SeedAction: ToolActionBase
    {
        public override bool OnApply(Vector2 worldPoint)
        {
            return false;
        }

        public override bool OnApplyToTileMap(Vector3Int tileMapPosition, TileMapReadController tileMapReadController)
        {
            if (!tileMapReadController.cropsManager.Check(tileMapPosition))
            {
                return false;
            }

            tileMapReadController.cropsManager.Seed(tileMapPosition);
            return true;
        }

        public override void OnItemUsed(Item itemUsed, ItemContainer inventory)
        {
            inventory.RemoveItem(itemUsed);
        }
    }
}
