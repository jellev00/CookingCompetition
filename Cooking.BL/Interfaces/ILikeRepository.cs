using Cooking.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cooking.BL.Interfaces
{
    public interface ILikeRepository
    {
        void AddLikeToRecipe(int recipeId, Like like);
        List<Like> GetLikesByRecipeId(int recipeId);
    }
}