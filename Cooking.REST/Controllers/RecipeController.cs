using Cooking.BL.Managers;
using Cooking.BL.Models;
using Cooking.EF.Models;
using Cooking.REST.Models.Input;
using Cooking.REST.Models.Output;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Cooking.REST.Controllers
{
    [EnableCors("AllowSpecificOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly RecipeManager _recipeManager;
        private readonly ChallengeManager _challengeManager;
        private readonly UserManager _userManager;
        private readonly ILogger _logger;

        public RecipeController(RecipeManager recipeManager, ChallengeManager challengeManager, UserManager userManager, ILoggerFactory loggerFactory)
        {
            _recipeManager = recipeManager;
            _challengeManager = challengeManager;
            _userManager = userManager;
            _logger = loggerFactory.AddFile("Logs/RecipeConLogs.txt").CreateLogger("Cooking - Recipe");
        }

        [HttpPost("Challenge/{challengeId}/User/{email}/Recipe")]
        public ActionResult<RecipeOutput> AddRecipeToChallenge(int challengeId, string email, RecipeInput r)
        {
            try
            {
                _logger.LogInformation($"AddRecipeToChallenge called: ChallengeId - {challengeId}, Email - {email}", challengeId, email);

                Challenge challenge = _challengeManager.GetChallengeById(challengeId);
                if (challenge == null)
                {
                    _logger.LogError($"Challenge with challengeId: {challengeId} could not be found!");
                    return NotFound($"Challenge with challengeId: {challengeId} could not be found!");
                }

                User user = _userManager.GetUserByEmail(email);
                if (user == null)
                {
                    _logger.LogError($"User with email: {email} could not be found!");
                    return NotFound($"User with email: {email} could not be found!");
                }

                Recipe recipe = new Recipe(r.RecipeName, r.RecipeDescription);
                if (_recipeManager.RecipeExists(recipe))
                {
                    _logger.LogError($"Recipe with name: {r.RecipeName} and description {r.RecipeDescription} already exists!");
                    return BadRequest($"Recipe with name: {r.RecipeName} and description {r.RecipeDescription} already exists!");
                }

                _recipeManager.AddRecipe(challengeId, email, recipe);

                Recipe recipeOutput = new Recipe
                (
                    recipe.RecipeId,
                    r.RecipeName,
                    r.RecipeDescription
                );

                _logger.LogInformation($"Recipe added successfully: {recipe.RecipeName}", recipe.RecipeName);
                return CreatedAtAction(nameof(GetRecipeById), new { recipeId = recipe.RecipeId }, recipeOutput);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error adding recipe: {ex.Message}", ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{recipeId}")]
        public ActionResult<Recipe> GetRecipeById(int recipeId)
        {
            try
            {
                _logger.LogInformation($"GetRecipeById called: RecipeId: {recipeId}");

                Recipe recipe = _recipeManager.GetRecipeById(recipeId);

                if (recipe == null)
                {
                    _logger.LogError($"Recipe with recipeId: {recipeId} could not be found!");
                    return NotFound($"Recipe with recipeId: {recipeId} could not be found!");
                }
                else
                {
                    _logger.LogInformation($"Recipe retrieved succesfully: RecipeId: {recipeId}");
                    return Ok(recipe);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving recipe: {ex.Message}", ex.Message);
                return BadRequest(ex.Message);
            }
        }

    }
}