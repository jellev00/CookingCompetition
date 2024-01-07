using Cooking.BL.Exceptions;
using Cooking.BL.Models;

namespace Cooking.BL.Tests
{
    public class ChallengeTest
    {

        [Fact]
        public void TestChallengeConstructorWithValidData()
        {
            // Arrange
            var challengeId = 1;
            var challengeName = "Cooking Challenge";
            var description = "Description of the challenge";
            var startDate = DateTime.Now.AddDays(1);
            var endDate = DateTime.Now.AddDays(5);
            var recipes = new List<Recipe> { /*...*/ };

            // Act
            var challenge = new Challenge(challengeId, challengeName, description, startDate, endDate, recipes);

            // Assert
            Assert.Equal(challengeId, challenge.ChallengeId);
            Assert.Equal(challengeName, challenge.ChallengeName);
            Assert.Equal(description, challenge.Description);
            Assert.Equal(startDate, challenge.StartDate);
            Assert.Equal(endDate, challenge.EndDate);
            Assert.Equal(recipes, challenge.Recipes);
        }

        [Fact]
        public void TestChallengeConstructorWithInvalidChallengeId()
        {
            // Arrange
            var challengeId = -1; // Invalid ID

            // Act & Assert
            var ex = Assert.Throws<ChallengeException>(() => new Challenge(challengeId, "Challenge", "Description", DateTime.Now.AddDays(1), DateTime.Now.AddDays(5), new List<Recipe>()));
            Assert.Equal("ID is invalid!", ex.Message);
        }

        [Fact]
        public void TestChallengeConstructorWithInvalidChallengeName()
        {
            // Arrange
            var challengeName = " "; // Invalid Name

            // Act & Assert
            var ex = Assert.Throws<ChallengeException>(() => new Challenge(challengeName, "Description", DateTime.Now.AddDays(1), DateTime.Now.AddDays(5), new List<Recipe>()));
            Assert.Equal("ChallengeName is invalid!", ex.Message);
        }

        [Fact]
        public void TestChallengeConstructorWithInvalidDescription()
        {
            // Arrange
            var description = " "; // Invalid Description

            // Act & Assert
            var ex = Assert.Throws<ChallengeException>(() => new Challenge("Challenge", description, DateTime.Now.AddDays(1), DateTime.Now.AddDays(5), new List<Recipe>()));
            Assert.Equal("Description is invalid!", ex.Message);
        }

        [Fact]
        public void TestChallengeConstructorWithPastStartDate()
        {
            // Arrange
            var pastDate = DateTime.Now.AddDays(-1); // Setting a date in the past
            var endDate = DateTime.Now.AddDays(1); // Setting a future end date

            // Act
            var exception = Record.Exception(() =>
            {
                var challenge = new Challenge("Challenge Name", "Description", pastDate, endDate, new List<Recipe>());
            });

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<ChallengeException>(exception);
            Assert.Equal("Start date can't be in the past!", exception.Message);
        }


        [Fact]
        public void TestChallengeConstructorWithEndDateBeforeStartDate()
        {
            // Arrange
            var startDate = DateTime.Now.AddDays(1);
            var endDate = DateTime.Now; // End Date before Start Date

            // Act & Assert
            var ex = Assert.Throws<ChallengeException>(() => new Challenge("Challenge", "Description", startDate, endDate, new List<Recipe>()));
            Assert.Equal("Date can't be in the past or earlier than the start date!", ex.Message);
        }

        //[Fact]
        //public void TestChallengeConstructorWithNullRecipes()
        //{
        //    // Arrange
        //    List<Recipe> recipes = null; // Null Recipes

        //    // Act & Assert
        //    var ex = Assert.Throws<ChallengeException>(() => new Challenge("Challenge", "Description", DateTime.Now.AddDays(1), DateTime.Now.AddDays(5), recipes));
        //    Assert.Equal("Recipes are invalid!", ex.Message);
        //}



        [Fact]
        public void TestAddRecipeWithNullRecipe()
        {
            // Arrange
            var challenge = new Challenge("Challenge", "Description", DateTime.Now.AddDays(1), DateTime.Now.AddDays(5), new List<Recipe>());

            // Act & Assert
            var ex = Assert.Throws<ChallengeException>(() => challenge.AddRecipe(null));
            Assert.Equal("AddRecipe - Recipe is null!", ex.Message);
        }
    }
}
