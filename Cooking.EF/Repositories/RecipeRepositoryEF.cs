using Cooking.BL.Interfaces;
using Cooking.BL.Models;
using Cooking.EF.Exceptions;
using Cooking.EF.Mappers;
using Cooking.EF.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Cooking.EF.Repositories
{
    public class RecipeRepositoryEF : IRecipeRepository
    {
        private readonly CookingContext ctx;

        public RecipeRepositoryEF(string connectionString)
        {
            this.ctx = new CookingContext(connectionString);
        }

        private void SaveAndClear()
        {
            ctx.SaveChanges();
            ctx.ChangeTracker.Clear();
        }

        public void AddRecipe(int challengeId, string email, Recipe recipe)
        {
            try
            {
                UserEF uEF = ctx.Users.Find(email);
                ChallengeEF cEF = ctx.Challenges.Find(challengeId);

                if (uEF == null)
                {
                    throw new RecipeRepositoryException($"AddRecipe: User met email {email} is niet gevonden");
                }

                if (cEF == null)
                {
                    throw new RecipeRepositoryException($"AddRecipe: Challenge met ID {challengeId} is niet gevonden");
                }

                RecipeEF newRecipe = MapRecipe.MapToDB(uEF, recipe, ctx);

                uEF.Recipes.Add(newRecipe);
                cEF.Recipes.Add(newRecipe);

                SaveAndClear();

                recipe.RecipeId = newRecipe.RecipeId;
            }
            catch (Exception ex)
            {
                throw new RecipeRepositoryException("AddRecipe", ex);
            }
        }

        public Recipe GetRecipeById(int recipeId)
        {
            try
            {
                RecipeEF recipeEF = ctx.Recipes.Where(x => x.RecipeId == recipeId)
                    .Include(x => x.User)
                    .AsNoTracking()
                    .FirstOrDefault();

                return MapRecipe.MapToDomain(recipeEF);
            }
            catch (Exception ex)
            {
                throw new RecipeRepositoryException("GetRecipeById", ex);
            }
        }
        public bool RecipeExists(Recipe recipe)
        {
            try
            {
                return ctx.Recipes.Any(x => x.RecipeName == recipe.RecipeName && x.RecipeDescription == recipe.Description);
            }
            catch (Exception ex)
            {
                throw new RecipeRepositoryException("UserExists", ex);
            }
        }
    }
}