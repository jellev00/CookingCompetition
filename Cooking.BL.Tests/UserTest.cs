using Cooking.BL.Exceptions;
using Cooking.BL.Models;

namespace Cooking.BL.Tests
{
    public class UserTest
    {
        [Fact]
        public void Constructor_WithValidParameters_InitializesProperties()
        {
            // Arrange
            var email = "test@example.com";
            var recipes = new List<Recipe>();

            // Act
            var user = new User(email, recipes);

            // Assert
            Assert.Equal(email, user.Email);
            Assert.Equal(recipes, user.Recipes);
        }

        [Fact]
        public void OverloadedConstructor_WithValidEmail_InitializesEmail()
        {
            // Arrange & Act
            var user = new User("test@example.com");

            // Assert
            Assert.Equal("test@example.com", user.Email);
            Assert.Null(user.Recipes);
        }

        [Fact]
        public void Email_SetValidValue_AssignsValue()
        {
            // Arrange
            var user = new User("initial@example.com");

            // Act
            user.Email = "test@example.com";

            // Assert
            Assert.Equal("test@example.com", user.Email);
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData("invalidemail")]
        public void Email_SetInvalidValue_ThrowsUserException(string invalidEmail)
        {
            // Arrange
            var user = new User("valid@example.com");

            // Act & Assert
            var ex = Assert.Throws<UserException>(() => user.Email = invalidEmail);
            Assert.Equal("Email is invalid!", ex.Message);
        }

        [Fact]
        public void Recipes_SetValidList_AssignsList()
        {
            // Arrange
            var user = new User("test@example.com");
            var recipes = new List<Recipe>();

            // Act
            user.Recipes = recipes;

            // Assert
            Assert.Equal(recipes, user.Recipes);
        }

        [Fact]
        public void Recipes_SetToNull_ThrowsRecipeException()
        {
            // Arrange
            var user = new User("test@example.com");

            // Act & Assert
            var ex = Assert.Throws<RecipeException>(() => user.Recipes = null);
            Assert.Equal("Recipes can't be null!", ex.Message);
        }


    }
}
