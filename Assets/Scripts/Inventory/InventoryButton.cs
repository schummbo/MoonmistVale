using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] Image icon;
    [SerializeField] Text amount;

    int index;

    public void SetIndex(int index)
    {
        this.index = index;
    }

    public void Set(ItemSlot itemSlot)
    {
        icon.gameObject.SetActive(true);
        icon.sprite = itemSlot.Item.Icon;

        if (itemSlot.Item.Stackable)
        {
            amount.gameObject.SetActive(true);
            amount.text = itemSlot.Count.ToString();
        }
        else
        {
            amount.gameObject.SetActive(false);
        }
    }

    public void Clean()
    {
        icon.sprite = null;
        icon.gameObject.SetActive(false);
        amount.gameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        var inventory = GameManager.Instance.InventoryContainer;

        GameManager.Instance.ItemDragDropController.OnClick(inventory.ItemSlots[index]);

        transform.parent.GetComponent<InventoryPanel>().Show();
    }
}
