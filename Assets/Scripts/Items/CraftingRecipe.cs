using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Recipe")]
public class CraftingRecipe : ScriptableObject
{
    public List<ItemSlot> Elements;

    public ItemSlot Output;
}
