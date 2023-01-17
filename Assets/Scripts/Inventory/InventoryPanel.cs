using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPanel : MonoBehaviour
{
    [SerializeField] ItemContainer inventory;
    [SerializeField] private List<InventoryButton> inventoryButtons;

    void Start()
    {
        SetIndexes();
    }

    void OnEnable()
    {
        Show();
    }

    private void SetIndexes()
    {
        for (int i = 0; i < inventory.ItemSlots.Count; i++)
        {
            inventoryButtons[i].SetIndex(i);
        }
    }

    public void Show()
    {
        for (int i = 0; i < inventory.ItemSlots.Count; i++)
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
}
