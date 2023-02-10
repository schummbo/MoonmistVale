using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts.Tools
{
    [CreateAssetMenu(menuName = "Data/Tool Actions/Plow Action")]
    public class PlowAction : ToolActionBase
    {
        [SerializeField] private List<TileBase> canPlow;

        public override bool OnApply(Vector2 worldPoint)
        {
            return false;
        }

        public override bool OnApplyToTileMap(Vector3Int tileMapPosition, TileMapReadController tileMapReadController, Item item)
        {
            var tile = tileMapReadController.GetTileBase(tileMapPosition);

            if (!canPlow.Contains(tile))
            {
                return false;
            }

            return tileMapReadController.cropsManager.Plow(tileMapPosition);
        }

        public override void OnItemUsed(Item itemUsed, ItemContainer inventory)
        {
        }
    }
}
