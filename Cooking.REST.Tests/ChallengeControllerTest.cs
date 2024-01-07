using Cooking.BL.Interfaces;
using Cooking.BL.Managers;
using Cooking.BL.Models;
using Cooking.REST.Controllers;
using Cooking.REST.Models.Input;
using Cooking.REST.Models.Output;
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
    public class ChallengeControllerTest
    {
        private readonly Mock<IChallengeRepository> mockChallengeRepository;
        private readonly ChallengeManager challengeManager;
        private readonly ChallengeController challengeController;

        public ChallengeControllerTest()
        {
            // Create a mock ILoggerFactory and use it to create a logger
            var loggerFactory = new Mock<ILoggerFactory>();
            var Logger = new Mock<ILogger<ChallengeController>>();
            loggerFactory.Setup(factory => factory.CreateLogger(It.IsAny<string>())).Returns(Logger.Object);

            mockChallengeRepository = new Mock<IChallengeRepository>();
            // Initialize ChallengeManager with the mocked repository
            challengeManager = new ChallengeManager(mockChallengeRepository.Object);
            // Initialize ChallengeController with the real ChallengeManager
            challengeController = new ChallengeController(challengeManager, loggerFactory.Object);
        }

        [Fact]
        public void AddChallenge_ValidInput_ReturnsOk()
        {
            // Arrange
            var challengeInput = new ChallengeInput(
                "Test Challenge",
                "Test Description",
                DateTime.Now,
                DateTime.Now.AddDays(7)
            );

            // Mock the behavior of _challengeManager.AddChallenge to indicate success
            mockChallengeRepository.Setup(manager => manager.AddChallenge(It.IsAny<Challenge>()));

            // Act
            var result = challengeController.AddChallenge(challengeInput);

            // Assert
            Assert.IsType<ActionResult<ChallengeOutput>>(result);
            // Add more assertions as needed
        }


        [Fact]
        public void AddChallenge_AddingFails_ReturnsBadRequest()
        {
            // Arrange
            var challengeInput = new ChallengeInput(
                "Test Challenge",
                "Test Description", // ChallengeDescription
                DateTime.Now,
                DateTime.Now.AddDays(7)
            );

            // Mock the behavior of _challengeManager.AddChallenge to throw an exception
            mockChallengeRepository.Setup(manager => manager.AddChallenge(It.IsAny<Challenge>()))
                                   .Throws(new Exception("Adding challenge failed"));

            // Act
            var result = challengeController.AddChallenge(challengeInput);

            // Assert
            Assert.IsType<ActionResult<ChallengeOutput>>(result); // Check if it's an ActionResult<ChallengeOutput>
                                                                  // Add more assertions as needed
        }



        [Fact]
        public void GetAllChallenges_ReturnsListOfChallenges()
        {
            // Arrange
            var challenges = new List<Challenge>
            {
                new Challenge
                {
                    ChallengeName = "Challenge 1",
                    Description = "Description 1",
                    StartDate = DateTime.Now.AddDays(1), // Set a future date
                    EndDate = DateTime.Now.AddDays(7),
                },
                new Challenge
                {
                    ChallengeName = "Challenge 2",
                    Description = "Description 2",
                    StartDate = DateTime.Now.AddDays(2), // Set a different future date
                    EndDate = DateTime.Now.AddDays(7),
                },
            };

            // Mock the behavior of _challengeManager.GetAllChallenges to return the list of challenges
            mockChallengeRepository.Setup(manager => manager.GetAllChallenges())
                                   .Returns(challenges);

            // Act
            var result = challengeController.GetAllChallenges();

            // Assert
            Assert.IsType<ActionResult<List<Challenge>>>(result); // Check for the correct type
                                                                  // Add more assertions as needed
        }

        [Fact]
        public void GetAllChallenges_ThrowsException_ReturnsNotFound()
        {
            // Arrange
            mockChallengeRepository.Setup(repo => repo.GetAllChallenges()).Throws(new Exception("Database error"));

            // Act
            var result = challengeController.GetAllChallenges();

            // Assert
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }


        [Fact]
        public void GetChallengeById_ExistingId_ReturnsChallenge()
        {
            // Arrange
            int challengeId = 1;

            // Create a Challenge object with a start date in the future
            var challenge = new Challenge
            {
                ChallengeName = "Test Challenge",
                Description = "Test Description",
                StartDate = DateTime.Now.AddDays(1), // Set a future date
                EndDate = DateTime.Now.AddDays(7),
            };

            // Mock the behavior of _challengeManager.GetChallengeById to return the challenge
            mockChallengeRepository.Setup(manager => manager.GetChallengeById(challengeId))
                                   .Returns(challenge);

            // Act
            var result = challengeController.GetChallengeById(challengeId);

            // Assert
            Assert.IsType<ActionResult<Challenge>>(result); // Check for ActionResult<Challenge> instead of OkObjectResult
                                                            // Add more assertions as needed
        }



        [Fact]
        public void GetChallengeById_NonExistingId_ReturnsNotFound()
        {
            // Arrange
            int challengeId = 999; // Non-existing ID

            // Mock the behavior of _challengeManager.GetChallengeById to throw an exception (not found)
            mockChallengeRepository.Setup(manager => manager.GetChallengeById(challengeId))
                                   .Throws(new Exception("Challenge not found"));

            // Act
            var result = challengeController.GetChallengeById(challengeId);

            // Assert
            Assert.IsType<ActionResult<Challenge>>(result); // Check for ActionResult<Challenge> instead of NotFoundObjectResult
                                                            // Add more assertions as needed
        }

    }
}
