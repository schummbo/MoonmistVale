using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] Image icon;
    [SerializeField] Text amount;
    [SerializeField] Image selectedImage;

    int index;

    public void Select(bool isSelected)
    {
        selectedImage.gameObject.SetActive(isSelected);
    }

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
        this.GetComponentInParent<ItemPanelBase>().OnClick(this.index);
    }
}
