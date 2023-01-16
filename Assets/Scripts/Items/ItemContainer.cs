using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Item Container")]
public class ItemContainer : ScriptableObject
{
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
    }
}
