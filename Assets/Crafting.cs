using System.Linq;
using UnityEngine;

public class Crafting : MonoBehaviour
{
    [SerializeField] private ItemContainer inventory;

    public void Craft(CraftingRecipe recipe)
    {
        if (!inventory.HasFreeSpace())
        {
            return;
        }

        bool canCraft = recipe.Elements.All(recipeElement => inventory.HasItems(recipeElement));

        if (!canCraft)
        {
            return;
        }

        foreach (var recipeElement in recipe.Elements)
        {
            inventory.RemoveItem(recipeElement.Item, recipeElement.Count);
        }
        
        inventory.Add(recipe.Output.Item, recipe.Output.Count);
    }
}
