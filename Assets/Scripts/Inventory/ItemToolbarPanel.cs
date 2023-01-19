public class ItemToolbarPanel : ItemPanelBase
{
    private ToolbarController toolbarController;

    void Start()
    {
        toolbarController = GetComponent<ToolbarController>();
        base.Init();
        toolbarController.OnChange += HandleSelectedToolChange;
        ChangeTool(0);
    }

    private void HandleSelectedToolChange(int obj)
    {
        ChangeTool(obj);
    }

    public override void OnClick(int index)
    {
        ChangeTool(index);
    }

    private void ChangeTool(int index)
    {
        toolbarController.SetSelectedTool(index);

        for (int i = 0; i < this.inventoryButtons.Count; i++)
        {
            inventoryButtons[i].Select(i == index);
        }
    }
}