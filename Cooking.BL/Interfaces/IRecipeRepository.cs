using Cooking.BL.Models;

namespace Cooking.BL.Interfaces
{
    public interface IRecipeRepository
    {
        void AddRecipe(int challengeId, string email, Recipe recipe);
        Recipe GetRecipeById(int recipeId);
        bool RecipeExists(Recipe recipe);
    }
}
