using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemPanelBase : MonoBehaviour
{
    [SerializeField] protected ItemContainer inventory;
    [SerializeField] protected List<InventoryButton> inventoryButtons;
    

    protected void Init()
    {
        SetIndexes();
        Show();
    }

    void Start()
    {
        Init();
    }

    void OnEnable()
    {
        Show();
        inventory.OnChange += InventoryChanged;
    }

    void OnDisable()
    {
        inventory.OnChange -= InventoryChanged;
    }

    private void InventoryChanged()
    {
        Show();
    }

    private void SetIndexes()
    {
        for (int i = 0; i < inventoryButtons.Count; i++)
        {
            inventoryButtons[i].SetIndex(i);
        }
    }

    public void Show()
    {
        for (int i = 0; i < inventoryButtons.Count; i++)
        {
            var slot = inventory.ItemSlots[i];

            if (slot.Item != null)
            {
                inventoryButtons[i].Set(slot);
            }
            else
            {
                inventoryButtons[i].Clean();
            }
        }
    }

    public abstract void OnClick(int index);
}
