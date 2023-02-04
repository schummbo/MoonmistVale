using Assets.Scripts.PubSub;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemPanelBase : MonoBehaviour
{
    [SerializeField] protected ItemContainer inventory;
    [SerializeField] protected List<InventoryButton> inventoryButtons;
    [SerializeField] protected PubSubEvents pubSubEvents;


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
        pubSubEvents.OnInventoryChange += InventoryChanged;
    }

    void OnDisable()
    {
        pubSubEvents.OnInventoryChange -= InventoryChanged;
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

    public virtual void Show()
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
