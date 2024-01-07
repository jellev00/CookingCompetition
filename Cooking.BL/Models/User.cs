using Cooking.BL.Exceptions;

namespace Cooking.BL.Models
{
    public class User
    {
        public User(string email, List<Recipe> recipes)
        {
            Email = email;
            Recipes = recipes;
        }

        // MapToDomain - 2

        public User(string email)
        {
            Email = email;
        }

        private string _email;
        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                if ((string.IsNullOrWhiteSpace(value)) || (!value.Contains('@')))
                {
                    throw new UserException("Email is invalid!");
                } else
                {
                    _email = value;
                }
            }
        }

        private List<Recipe> _recipes;
        public List<Recipe> Recipes
        {
            get
            {
                return _recipes;
            }
            set
            {
                if (value == null)
                {
                    throw new RecipeException("Recipes can't be null!");
                }
                else
                {
                    _recipes = value;
                }
            }
        }
    }
}