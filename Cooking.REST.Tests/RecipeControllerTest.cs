using Cooking.BL.Interfaces;
using Cooking.BL.Managers;
using Cooking.BL.Models;
using Cooking.REST.Controllers;
using Cooking.REST.Models.Input;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cooking.REST.Tests
{
    public class RecipeControllerTest
    {
        private readonly Mock<IRecipeRepository> mockRecipeRepository;
        private readonly Mock<IChallengeRepository> mockChallengeRepository;
        private readonly Mock<IUserRepository> mockUserRepository;
        private readonly RecipeManager recipeManager;
        private readonly ChallengeManager challengeManager;
        private readonly UserManager userManager;
        private readonly RecipeController recipeController;
        private readonly Mock<ILogger<RecipeController>> mockLogger;

        public RecipeControllerTest()
        {
            mockRecipeRepository = new Mock<IRecipeRepository>();
            mockChallengeRepository = new Mock<IChallengeRepository>();
            mockUserRepository = new Mock<IUserRepository>();
            mockLogger = new Mock<ILogger<RecipeController>>();

            // Zorg ervoor dat alle afhankelijkheden correct worden gemockt en doorgegeven
            var loggerFactory = new Mock<ILoggerFactory>();
            loggerFactory.Setup(factory => factory.CreateLogger(It.IsAny<string>())).Returns(mockLogger.Object);

            recipeManager = new RecipeManager(mockRecipeRepository.Object);
            challengeManager = new ChallengeManager(mockChallengeRepository.Object);
            userManager = new UserManager(mockUserRepository.Object);

            recipeController = new RecipeController(recipeManager, challengeManager, userManager, loggerFactory.Object);
        }

        [Fact]
        public void AddRecipeToChallenge_Success_ReturnsOk()
        {
            // Arrange
            var challengeId = 1;
            var email = "test@example.com";
            var recipeInput = new RecipeInput ("Test Recipe", "Description" );

            // Act
            var result = recipeController.AddRecipeToChallenge(challengeId, email, recipeInput);

            // Assert
            Assert.IsType<OkResult>(result.Result);
        }

        [Fact]
        public void AddRecipeToChallenge_Failure_ReturnsBadRequest()
        {
            // Arrange
            var challengeId = 1;
            var email = "test@example.com";
            var recipeInput = new RecipeInput("Test Recipe", "Description");
            mockRecipeRepository.Setup(repo => repo.AddRecipe(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<Recipe>()))
                                .Throws(new Exception("Database error"));

            // Act
            var result = recipeController.AddRecipeToChallenge(challengeId, email, recipeInput);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }


    }
}
