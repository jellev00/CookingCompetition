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
    public class LikeControllerTest
    {
        private readonly Mock<ILikeRepository> mockLikeRepository;
        private readonly LikeManager likeManager;
        private readonly LikeController likeController;

        public LikeControllerTest()
        {
            // Create a mock ILoggerFactory and use it to create a logger
            var loggerFactory = new Mock<ILoggerFactory>();
            var Logger = new Mock<ILogger<LikeController>>();
            loggerFactory.Setup(factory => factory.CreateLogger(It.IsAny<string>())).Returns(Logger.Object);
            mockLikeRepository = new Mock<ILikeRepository>();
            // Initialize LikeManager with the mocked repository
            likeManager = new LikeManager(mockLikeRepository.Object);
            // Initialize LikeController with the real LikeManager
            likeController = new LikeController(likeManager, loggerFactory.Object);
        }

        [Fact]
        public void AddLikeToRecipe_ReturnsLike_WhenAddIsSuccessful()
        {
            // Arrange
            int recipeId = 1;
            var likeInput = new LikeInput { /* Initialize with necessary data */ };
            var like = new Like(DateTime.Now);
            mockLikeRepository.Setup(repo => repo.AddLikeToRecipe(recipeId, like));

            // Act
            var result = likeController.AddLikeToRecipe(recipeId, likeInput);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<Like>(okResult.Value);
            Assert.NotNull(returnValue);
        }

        //[Fact]
        //public void AddLikeToRecipe_ThrowsException_ReturnsBadRequest()
        //{
        //    // Arrange
        //    int recipeId = 1;
        //    var likeInput = new LikeInput(); // Assuming LikeInput is a valid input model
        //    var exceptionMessage = "Database error";

        //    mockLikeRepository.Setup(manager => manager.AddLikeToRecipe(recipeId, It.IsAny<Like>()))
        //                   .Throws(new Exception(exceptionMessage));

        //    // Act
        //    var result = likeController.AddLikeToRecipe(recipeId, likeInput);

        //    // Assert
        //    var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        //    Assert.Equal(exceptionMessage, badRequestResult.Value);
        //}

        //[Fact]
        //public void GetLikesByRecipeId_ReturnsLikes_WhenLikesExist()
        //{
        //    // Arrange
        //    int recipeId = 1;
        //    var likes = new List<Like> { /* Voeg hier mock like-objecten toe */ };
        //    mockLikeRepository.Setup(repo => repo.GetLikesByRecipeId(recipeId)).Returns(likes);

        //    // Act
        //    var result = likeController.GetLikesByRecipeId(recipeId);

        //    // Assert
        //    Assert.NotNull(result);
        //    var okResult = Assert.IsType<OkObjectResult>(result.Result);
        //    Assert.Equal(likes, okResult.Value);
        //}

        [Fact]
        public void GetLikesByRecipeId_ReturnsNotFound_WhenExceptionOccurs()
        {
            // Arrange
            int recipeId = 1;
            mockLikeRepository.Setup(repo => repo.GetLikesByRecipeId(recipeId)).Throws(new Exception("Database error"));

            // Act
            var result = likeController.GetLikesByRecipeId(recipeId);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

    }
}
