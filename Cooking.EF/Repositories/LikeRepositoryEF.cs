using Cooking.BL.Interfaces;
using Cooking.BL.Models;
using Cooking.EF.Exceptions;
using Cooking.EF.Mappers;
using Cooking.EF.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cooking.EF.Repositories
{
    public class LikeRepositoryEF : ILikeRepository
    {
        private readonly CookingContext ctx;

        public LikeRepositoryEF(string connectionString)
        {
            this.ctx = new CookingContext(connectionString);
        }

        private void SaveAndClear()
        {
            ctx.SaveChanges();
            ctx.ChangeTracker.Clear();
        }

        public void AddLikeToRecipe(int recipeId, Like like)
        {
            try
            {
                RecipeEF recipeEF = ctx.Recipes
                    .FirstOrDefault(x => x.RecipeId == recipeId);

                LikeEF likeEF = MapLike.MapToDB(like);
                recipeEF.Likes.Add(likeEF);

                SaveAndClear();
            }
            catch (Exception ex)
            {
                throw new RecipeRepositoryException("AddLikeToRecipe", ex);
            }
        }

        public List<Like> GetLikesByRecipeId(int recipeId)
        {
            try
            {
                RecipeEF recipeEF = ctx.Recipes
                    .Include(x => x.Likes)
                    .FirstOrDefault(x => x.RecipeId == recipeId);

                if (recipeEF == null)
                {
                    throw new RecipeRepositoryException($"GetLikesByRecipeId - Recipe with ID {recipeId} not found.");
                }

                List<Like> likes = recipeEF.Likes.Select(MapLike.MapToDomain).ToList();

                return likes;
            }
            catch (Exception ex)
            {
                throw new RecipeRepositoryException("GetLikesByRecipeId", ex);
            }
        }
    }
}
