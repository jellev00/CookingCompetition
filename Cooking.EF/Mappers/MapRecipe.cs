using Cooking.BL.Models;
using Cooking.EF.Exceptions;
using Cooking.EF.Models;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using Image = Cooking.BL.Models.Image;

namespace Cooking.EF.Mappers
{
    public class MapRecipe
    {
        public static RecipeEF MapToDB(UserEF u, Recipe r, CookingContext ctx)
        {
            try
            {
                List<ImageEF> images = new List<ImageEF>();

                if (r.Images != null)
                {
                    foreach (Image i in r.Images)
                    {
                        ImageEF imageEF = new ImageEF(i.ImageUrl);
                        images.Add(imageEF);
                    }
                }

                RecipeEF recipeEF = new RecipeEF(r.RecipeName, r.Description, images, u);

                return recipeEF;

            }
            catch (Exception ex)
            {
                throw new MapException("MapRecipe - MapToDB", ex);
            }
        }

        public static Recipe MapToDomain(RecipeEF recipeEF)
        {
            try
            {
                Recipe recipe = new Recipe(recipeEF.RecipeId, recipeEF.RecipeName, recipeEF.RecipeDescription, null, new User(recipeEF.User.Email), null);

                List<Image> images = new List<Image>();
                if (recipeEF.Images != null)
                {
                    foreach (ImageEF imageEF in recipeEF.Images)
                    {
                        images.Add(MapImage.MapToDomain(imageEF));
                        recipe.Images = images;
                    }
                }

                if (recipeEF.Likes != null)
                {
                    List<Like> likes = new List<Like>();
                    foreach (LikeEF likeEF in recipeEF.Likes)
                    {
                        likes.Add(MapLike.MapToDomain(likeEF));
                        recipe.Likes = likes;
                    }
                }


                return recipe;
            }
            catch (Exception ex)
            {
                throw new MapException("MapRecipe - MapToDomain", ex);
            }
        }
    }
}