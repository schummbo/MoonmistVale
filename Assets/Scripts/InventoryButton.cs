using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryButton : MonoBehaviour
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
}
