using Cooking.BL.Models;

namespace Cooking.REST.Models.Input
{
    public class RecipeInput
    {
        public RecipeInput(string recipeName, string recipeDescription)
        {
            RecipeName = recipeName;
            RecipeDescription = recipeDescription;
        }

        public string RecipeName { get; set; }
        public string RecipeDescription { get; set; }
    }
}