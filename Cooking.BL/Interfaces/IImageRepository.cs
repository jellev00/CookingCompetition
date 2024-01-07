using Cooking.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cooking.BL.Interfaces
{
    public interface IImageRepository
    {
        List<Image> GetImagesFromRecipe(int recipeId);
        void AddImagesToRecipe(int recipeId, Image images);
    }
}
