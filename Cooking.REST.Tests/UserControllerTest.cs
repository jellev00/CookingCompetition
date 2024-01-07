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
    public class UserControllerTest
    {
        private readonly Mock<IUserRepository> mockUserRepository;
        private readonly UserManager userManager;
        private readonly UserController userController;

        public UserControllerTest()
        {
            // Create a mock ILoggerFactory and use it to create a logger
            var loggerFactory = new Mock<ILoggerFactory>();
            var logger = new Mock<ILogger<UserController>>();
            loggerFactory.Setup(factory => factory.CreateLogger(It.IsAny<string>())).Returns(logger.Object);

            mockUserRepository = new Mock<IUserRepository>();
            // Initialize UserManager with the mocked repository
            userManager = new UserManager(mockUserRepository.Object);
            // Initialize UserController with the real UserManager
            userController = new UserController(userManager, loggerFactory.Object);
        }

        [Fact]
        public void GetUserByEmail_UserExists_ReturnsUser()
        {
            // Arrange
            var email = "test@example.com";
            var expectedUser = new User(email);
            mockUserRepository.Setup(m => m.GetUserByEmail(email)).Returns(expectedUser);

            // Act
            var result = userController.GetUserByEmail(email);

            // Assert
            Assert.NotNull(result); // First, assert that the result is not null
            var okResult = Assert.IsType<OkObjectResult>(result.Result); // Then, assert the type
            var returnedUser = Assert.IsType<User>(okResult.Value);
            Assert.Equal(expectedUser, returnedUser);
        }

        [Fact]
        public void GetUserByEmail_UserDoesNotExist_ReturnsNotFound()
        {
            // Arrange
            var email = "nonexistent@example.com";
            mockUserRepository.Setup(repo => repo.GetUserByEmail(email)).Returns((User)null);

            // Act
            var result = userController.GetUserByEmail(email);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public void AddUser_ValidUser_ReturnsOk()
        {
            // Arrange
            var userInput = new UserInput("newuser@example.com"); // Passing email as a constructor argument
            mockUserRepository.Setup(repo => repo.AddUser(It.IsAny<User>()));

            // Act
            var result = userController.AddUser(userInput);

            // Assert
            Assert.IsType<OkResult>(result.Result);
        }

        [Fact]
        public void AddUser_InvalidUser_ReturnsBadRequest()
        {
            // Arrange
            var userInput = new UserInput(""); // Passing an empty email string as a constructor argument
            mockUserRepository.Setup(repo => repo.AddUser(It.IsAny<User>()))
                              .Throws(new Exception("Invalid user"));

            // Act
            var result = userController.AddUser(userInput);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }
    }
}
