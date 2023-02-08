using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Items;
using UnityEngine;

public class RecipePanel : ItemPanelBase
{
    [SerializeField] private RecipeList recipeList;

    public override void OnClick(int index)
    {
        var recipe = recipeList.Recipes[index];

        GameManager.Instance.Player.GetComponent<Crafting>().Craft(recipe);
    }

    public override void Show()
    {
        for (int i = 0; i < inventoryButtons.Count && i < recipeList.Recipes.Count; i++)
        {
            inventoryButtons[i].Set(recipeList.Recipes[i].Output);
        }
    }
}
