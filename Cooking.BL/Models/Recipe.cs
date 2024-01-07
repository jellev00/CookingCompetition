using Cooking.BL.Exceptions;
using Cooking.BL.Managers;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using static System.Net.Mime.MediaTypeNames;

namespace Cooking.BL.Models
{
    public class Recipe
    {
        public Recipe(int recipeId, string recipeName, string description, List<Like> likes, User user, List<Image> images)
        {
            RecipeId = recipeId;
            RecipeName = recipeName;
            Description = description;
            Likes = likes;
            User = user;
            Images = images;
        }

        public Recipe(string recipeName, string description, List<Like> likes, User user, List<Image> images)
        {
            RecipeName = recipeName;
            Description = description;
            Likes = likes;
            User = user;
            Images = images;
        }

        public Recipe(string recipeName, string description)
        {
            RecipeName = recipeName;
            Description = description;
        }

        public Recipe(int recipeId, string recipeName, string description)
        {
            RecipeId = recipeId;
            RecipeName = recipeName;
            Description = description;
        }

        //public Recipe(int recipeId, string recipeName, string description, User user)
        //{
        //    RecipeId = recipeId;
        //    RecipeName = recipeName;
        //    Description = description;
        //    User = user;
        //}

        //public Recipe(string recipeName, string description, User user)
        //{
        //    RecipeName = recipeName;
        //    Description = description;
        //    User = user;
        //}

        //// MapToDomain - 2
        //public Recipe(int recipeId, string recipeName, string recipeDescription, User user, List<Image> images)
        //{
        //    RecipeId = recipeId;
        //    RecipeName = recipeName;
        //}

        //public Recipe(string recipeName, string recipeDescription)
        //{
        //    RecipeName = recipeName;
        //}

        //public Recipe(string recipeName, string recipeDescription, User user, List<Image> images) : this(recipeName, recipeDescription)
        //{
        //}

        //public Recipe(string recipeName, string recipeDescription, List<Image> images) : this(recipeName, recipeDescription)
        //{
        //}

        //public Recipe(string recipeName, string recipeDescription, User user) : this(recipeName, recipeDescription)
        //{
        //}

        private int _recipeId;
        public int RecipeId
        {
            get
            {
                return _recipeId;
            }
            set
            {
                if (value <= 0)
                {
                    throw new RecipeException("RecipeID is invalid!");
                }
                else
                {
                    _recipeId = value;
                }
            }
        }

        private string _recipeName;
        public string RecipeName
        {
            get
            {
                return _recipeName;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new RecipeException("RecipeName is invalid!");
                }
                else
                {
                    _recipeName = value;
                }
            }
        }

        private string _description;
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new RecipeException("RecipeDescription is invalid!");
                }
                else
                {
                    _description = value;
                }
            }
        }

        private List<Like> _likes;
        public List<Like> Likes
        {
            get
            {
                return _likes;
            }
            set
            {
                _likes = value;
            }
        }

        private User _user;
        public User User
        {
            get
            {
                return _user;
            }
            set
            {
                if (value == null)
                {
                    throw new RecipeException("User is invalid!");
                }
                else
                {
                    _user = value;
                }
            }
        }

        private List<Image> _images;
        public List<Image> Images
        {
            get
            {
                return _images;
            }
            set
            {
                _images = value;
            }
        }

        public void AddImage(Image image)
        {
            if (image == null)
            {
                throw new RecipeException("AddImage - Image is null");
            }
            if (_images.Contains(image))
            {
                throw new RecipeException("AddImage - Image already is added!");
            }
            else
            {
                _images.Add(image);
            }
        }

        public void AddLike(Like like)
        {
            if (like == null)
            {
                throw new RecipeException("AddLike - Like is null");
            }
            if (_likes.Contains(like))
            {
                throw new RecipeException("AddLike - Like already is added!");
            }
            else
            {
                _likes.Add(like);
            }
        }
    }
}