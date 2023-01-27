using Assets.Scripts.Crops;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Item")]
public class Item : ScriptableObject
{
    public string Name;
    public bool Stackable;
    public Sprite Icon;
    public ToolActionBase onAction;
    public ToolActionBase onTilemapAction;
    public ToolActionBase onItemUsed;
    public CropData CropData;
}
