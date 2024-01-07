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
    public class ImageRepositoryEF : IImageRepository
    {
        private readonly CookingContext ctx;

        public ImageRepositoryEF(string connectionString)
        {
            this.ctx = new CookingContext(connectionString);
        }

        public List<Image> GetImagesFromRecipe(int recipeId)
        {
            try
            {
                RecipeEF recipeEF = ctx.Recipes.Include(x => x.Images).Include(x => x.User).AsNoTracking().FirstOrDefault(x => x.RecipeId == recipeId);

                Recipe recipeCorrect = MapRecipe.MapToDomain(recipeEF);

                return recipeCorrect.Images;
            }
            catch (Exception ex)
            {
                throw new ImageRepositoryException("GetImagesFromRecipe", ex);
            }
        }

        public void AddImagesToRecipe(int recipeId, Image images)
        {
            try
            {
                RecipeEF recipeEF = ctx.Recipes.Include(x => x.Images).FirstOrDefault(x => x.RecipeId == recipeId);

                ImageEF imageEF = MapImage.MapToDB(images);
                recipeEF.Images.Add(imageEF);
                ctx.Images.Add(imageEF);
                SaveAndClear();
            }
            catch (Exception ex)
            {
                throw new ImageRepositoryException("AddImagesToRecipe", ex);
            }
        }

        private void SaveAndClear()
        {
            ctx.SaveChanges();
            ctx.ChangeTracker.Clear();
        }
    }
}