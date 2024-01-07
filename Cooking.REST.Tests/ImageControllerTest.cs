using Cooking.BL.Interfaces;
using Cooking.BL.Managers;
using Cooking.BL.Models;
using Cooking.REST.Controllers;
using Microsoft.AspNetCore.Http;
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
    public class ImageControllerTest
    {
        private readonly Mock<IImageRepository> mockImageRepository;
        private readonly ImageManager imageManager;
        private readonly ImageController imageController;

        public ImageControllerTest()
        {
            // Create a mock ILoggerFactory and use it to create a logger
            var loggerFactory = new Mock<ILoggerFactory>();
            var Logger = new Mock<ILogger<ImageController>>();
            loggerFactory.Setup(factory => factory.CreateLogger(It.IsAny<string>())).Returns(Logger.Object);

            mockImageRepository = new Mock<IImageRepository>();
            // Initialize ImageManager with the mocked repository
            imageManager = new ImageManager(mockImageRepository.Object);
            // Initialize ImageController with the real ImageManager
            imageController = new ImageController(imageManager, loggerFactory.Object);
        }

        [Fact]
        public void GetImageByRecipeId_ReturnsImages_WhenImagesExist()
        {
            // Arrange
            var recipeId = 1;
            var images = new List<Image> { new Image("imageUrl1"), new Image("imageUrl2") };
            mockImageRepository.Setup(manager => manager.GetImagesFromRecipe(recipeId)).Returns(images);

            // Act
            var result = imageController.GetImageByRecipeId(recipeId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<Image>>(okResult.Value);
            Assert.Equal(images.Count, returnValue.Count);
        }

        [Fact]
        public void GetImageByRecipeId_ThrowsException_ReturnsInternalServerError()
        {
            // Arrange
            int recipeId = 1;
            mockImageRepository.Setup(manager => manager.GetImagesFromRecipe(recipeId)).Throws(new Exception("Database error"));

            // Act
            var result = imageController.GetImageByRecipeId(recipeId);

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result.Result);
            Assert.Equal(StatusCodes.Status500InternalServerError, statusCodeResult.StatusCode);
            Assert.Equal("Internal Server Error", statusCodeResult.Value);
        }

        // ik kan dit niet testen door opslaan van image in loacle omgeving

        //[Fact]
        //public async Task AddImage_Success_ReturnsOk()
        //{
        //    // Arrange
        //    int recipeId = 1;
        //    var mockImageFile = new Mock<IFormFile>();
        //    // Setup mock file details if necessary (FileName, Length, etc.)

        //    // Act
        //    var result = await imageController.AddImage(mockImageFile.Object, recipeId);

        //    // Assert
        //    var objectResult = Assert.IsType<ObjectResult>(result);
        //    Assert.Equal("Image uploaded successfully", objectResult.Value);
        //}

        [Fact]
        public async Task AddImage_ThrowsException_ReturnsInternalServerError()
        {
            // Arrange
            int recipeId = 1;
            var mockImageFile = new Mock<IFormFile>();
            mockImageRepository.Setup(repo => repo.AddImagesToRecipe(It.IsAny<int>(), It.IsAny<Image>()))
                               .Throws(new Exception("Some error"));

            // Act
            var result = await imageController.AddImage(mockImageFile.Object, recipeId);

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, statusCodeResult.StatusCode);
        }


        
    }
}
