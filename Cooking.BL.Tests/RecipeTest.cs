using Cooking.BL.Exceptions;
using Cooking.BL.Models;

namespace Cooking.BL.Tests
{
    public class RecipeTest
    {

        [Fact]
        public void Constructor_WithValidParameters_InitializesProperties()
        {
            // Arrange
            var recipeId = 1;
            var recipeName = "Test Recipe";
            var description = "Test Description";
            var likes = new List<Like>();
            var user = new User("user@example.com");
            var images = new List<Image>();

            // Act
            var recipe = new Recipe(recipeId, recipeName, description, likes, user, images);

            // Assert
            Assert.Equal(recipeId, recipe.RecipeId);
            Assert.Equal(recipeName, recipe.RecipeName);
            Assert.Equal(description, recipe.Description);
            Assert.Equal(likes, recipe.Likes);
            Assert.Equal(user, recipe.User);
            Assert.Equal(images, recipe.Images);
        }

        [Fact]
        public void OverloadedConstructor_WithValidParameters_InitializesProperties()
        {
            // Arrange
            var recipeName = "Test Recipe";
            var description = "Test Description";

            // Act
            var recipe = new Recipe(recipeName, description);

            // Assert
            Assert.Equal(recipeName, recipe.RecipeName);
            Assert.Equal(description, recipe.Description);
            Assert.Null(recipe.Likes);
            Assert.Null(recipe.User);
            Assert.Null(recipe.Images);
        }

        [Fact]
        public void RecipeId_SetValidValue_AssignsValue()
        {
            // Arrange
            var recipe = new Recipe("Test", "Description");

            // Act
            recipe.RecipeId = 10;

            // Assert
            Assert.Equal(10, recipe.RecipeId);
        }


        [Fact]
        public void RecipeId_SetInvalidValue_ThrowsRecipeException()
        {
            // Arrange
            var recipe = new Recipe("Test", "Description");

            // Act & Assert
            var ex = Assert.Throws<RecipeException>(() => recipe.RecipeId = 0);
            Assert.Equal("RecipeID is invalid!", ex.Message);
        }


        [Fact]
        public void RecipeName_SetValidValue_AssignsValue()
        {
            // Arrange
            var recipe = new Recipe("Initial Name", "Description");

            // Act
            recipe.RecipeName = "Test Recipe";

            // Assert
            Assert.Equal("Test Recipe", recipe.RecipeName);
        }

        [Fact]
        public void RecipeName_SetInvalidValue_ThrowsRecipeException()
        {
            // Arrange
            var recipe = new Recipe("Initial Name", "Description");

            // Act & Assert
            var ex = Assert.Throws<RecipeException>(() => recipe.RecipeName = "");
            Assert.Equal("RecipeName is invalid!", ex.Message);
        }


        [Fact]
        public void AddImage_ValidImage_AddsImageToList()
        {
            // Arrange
            var recipe = new Recipe("Name", "Description");
            var imageUrl = "http://example.com/image.jpg";
            var image = new Image(imageUrl);
             recipe.Images = new List<Image>();

            // Act
            recipe.AddImage(image);

            // Assert
            Assert.Contains(image, recipe.Images);
        }


        [Fact]
        public void AddImage_NullImage_ThrowsRecipeException()
        {
            // Arrange
            var recipe = new Recipe("Name", "Description");

            // Act & Assert
            var ex = Assert.Throws<RecipeException>(() => recipe.AddImage(null));
            Assert.Equal("AddImage - Image is null", ex.Message);
        }

        [Fact]
        public void AddLike_ValidLike_AddsLikeToList()
        {
            // Arrange
            var recipe = new Recipe("Name", "Description");
            var likeDate = DateTime.Now;
            var like = new Like(1, likeDate);

            // If Likes is not initialized in the Recipe constructor, initialize it here
            recipe.Likes = new List<Like>();

            // Act
            recipe.AddLike(like);

            // Assert
            Assert.Contains(like, recipe.Likes);
        }


        [Fact]
        public void AddLike_NullLike_ThrowsRecipeException()
        {
            // Arrange
            var recipe = new Recipe("Name", "Description");

            // Act & Assert
            var ex = Assert.Throws<RecipeException>(() => recipe.AddLike(null));
            Assert.Equal("AddLike - Like is null", ex.Message);
        }

    }
}
