using System.Collections.Generic;
using Assets.Scripts.PubSub;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Item Container")]
public class ItemContainer : ScriptableObject
{
    [SerializeField] private PubSubEvents pubSubEvents;

    public List<ItemSlot> ItemSlots;

    public void Add(Item item, int count = 1)
    {
        if (item.Stackable)
        {
            var slot = ItemSlots.Find(slot => slot.Item == item);

            if (slot != null)
            {
                slot.Count += count;
            }
            else
            {
                var emptySlot = ItemSlots.Find(slot => slot.Item == null);
                emptySlot.Count = count;
                emptySlot.Item = item;
            }
        }
        else
        {
            var emptySlot = ItemSlots.Find(slot => slot.Item == null);
            if (emptySlot != null)
            {
                emptySlot.Item = item;
            }
        }

        pubSubEvents.OnInventoryChange?.Invoke();
    }

    public void RemoveItem(Item item, int itemCount = 1)
    {
        if (item.Stackable)
        {
            var itemSlot = this.ItemSlots.Find(slot => slot.Item == item);

            if (itemSlot == null)
            {
                return; 
            }

            itemSlot.Count -= itemCount;

            if (itemSlot.Count <= 0)
            {
                itemSlot.Clear();
            }
        }
        else
        {
            while (itemCount > 0)
            {
                itemCount--;

                var itemSlot = this.ItemSlots.Find(slot => slot.Item == item);

                if (itemSlot == null)
                {
                    break;
                }
            }
        }

        pubSubEvents.OnInventoryChange?.Invoke();
    }
}
