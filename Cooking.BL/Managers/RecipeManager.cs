using Cooking.BL.Exceptions;
using Cooking.BL.Interfaces;
using Cooking.BL.Models;

namespace Cooking.BL.Managers
{
    public class RecipeManager
    {
        private readonly IRecipeRepository _repo;

        public RecipeManager(IRecipeRepository repo)
        {
            _repo = repo;
        }

        public void AddRecipe(int challengeId, string email, Recipe recipe)
        {
            try
            {
                _repo.AddRecipe(challengeId, email, recipe);
            }
            catch (Exception ex)
            {
                throw new RecipeManagerException("AddRecipe", ex);
            }
        }

        public Recipe GetRecipeById(int recipeId)
        {
            try
            {
                return _repo.GetRecipeById(recipeId);
            }
            catch (Exception ex)
            {
                throw new RecipeManagerException("GetRecipeById", ex);
            }
        }

        public bool RecipeExists(Recipe recipe)
        {
            try
            {
                return _repo.RecipeExists(recipe);
            }
            catch (Exception ex)
            {
                throw new RecipeManagerException("RecipeExists", ex);
            }
        }
    }
}