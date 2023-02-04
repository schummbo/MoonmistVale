public class InventoryPanel : ItemPanelBase
{
    public override void OnClick(int index)
    {
        GameManager.Instance.ItemDragDropController.OnClick(this.inventory.ItemSlots[index]);
        Show();
    }
}
