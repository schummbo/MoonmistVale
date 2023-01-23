using System.Collections.Generic;
using Assets.Scripts.ToolHittables;
using UnityEngine;

public abstract class ToolActionBase : ScriptableObject
{
    public abstract bool OnApply(Vector2 worldPoint);

    public abstract bool OnApplyToTileMap(Vector3Int tileMapPosition, TileMapReadController tileMapReadController);

    public abstract void OnItemUsed(Item itemUsed, ItemContainer inventory);

}
