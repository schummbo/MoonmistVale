using System;

[Serializable]
public class ItemSlot
{
    public Item Item;
    public int Count;

    public void Copy(ItemSlot itemSlot)
    {
        this.Item = itemSlot.Item;
        this.Count = itemSlot.Count;
    }

    public void Clear()
    {
        this.Item = null;
        this.Count = 0;
    }
}