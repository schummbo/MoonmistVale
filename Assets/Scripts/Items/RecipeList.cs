using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Items
{
    [CreateAssetMenu(menuName = "Data/Recipe List")]
    public class RecipeList : ScriptableObject
    {
        public List<CraftingRecipe> Recipes;
    }
}
