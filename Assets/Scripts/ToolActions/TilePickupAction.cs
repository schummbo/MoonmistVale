using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Tool Actions/Tile Pickup Action")]
public class TilePickupAction : ToolActionBase
{
    public override bool OnApply(Vector2 worldPoint)
    {
        return false;
    }

    public override bool OnApplyToTileMap(Vector3Int tileMapPosition, TileMapReadController tileMapReadController, Item item)
    {
        return tileMapReadController.cropsManager.PickUp(tileMapPosition);
    }

    public override void OnItemUsed(Item itemUsed, ItemContainer inventory)
    {
    }
}
