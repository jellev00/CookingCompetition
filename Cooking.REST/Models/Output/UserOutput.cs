using Cooking.BL.Models;
using Cooking.EF.Models;

namespace Cooking.REST.Models.Output
{
    public class UserOutput
    {
        public UserOutput(string email, List<Recipe> recipes)
        {
            Email = email;
            Recipes = recipes;
        }

        public string Email { get; set; }
        public List<Recipe> Recipes { get; set; } = new List<Recipe>();
    }
}