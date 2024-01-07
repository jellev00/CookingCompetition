using Cooking.BL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cooking.EF.Models
{
    public class RecipeEF
    {
        public RecipeEF()
        {

        }

        public RecipeEF(string recipeName, string recipeDescription, List<ImageEF> images, UserEF user)
        {
            RecipeName = recipeName;
            RecipeDescription = recipeDescription;
            Images = images;
            User = user;
        }

        public RecipeEF(int recipeId, string recipeName, string recipeDescription, List<ImageEF> images, UserEF user, List<LikeEF> likes)
        {
            RecipeId = recipeId;
            RecipeName = recipeName;
            RecipeDescription = recipeDescription;
            Images = images;
            User = user;
            Likes = likes;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RecipeId { get; set; }
        public string RecipeName { get; set; }
        public string RecipeDescription { get; set; }
        public List<ChallengeEF> Challenges { get; set; } = new List<ChallengeEF>();
        public List<ImageEF> Images { get; set; } = new List<ImageEF>();
        public UserEF User { get; set; }
        public List<LikeEF> Likes { get; set; } = new List<LikeEF>();
    }
}