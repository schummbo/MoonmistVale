using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPanel : ItemPanelBase
{
    public override void OnClick(int index)
    {
        GameManager.Instance.ItemDragDropController.OnClick(this.inventory.ItemSlots[index]);
        Show();
    }
}
