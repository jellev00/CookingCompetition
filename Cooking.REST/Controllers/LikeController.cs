using Cooking.BL.Managers;
using Cooking.BL.Models;
using Cooking.REST.Models.Input;
using Cooking.REST.Models.Output;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cooking.REST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikeController : ControllerBase
    {
        private LikeManager _likeManager;
        private readonly RecipeManager _recipeManager;
        private readonly ILogger _logger;

        public LikeController(LikeManager likeManager, RecipeManager recipeManager, ILoggerFactory loggerFactory)
        {
            _likeManager = likeManager;
            _recipeManager = recipeManager;
            _logger = loggerFactory.AddFile("Logs/LikeConLogs.txt").CreateLogger("Cooking - Like");
        }

        [HttpPost("Recipe/{recipeId}/Like")]
        public ActionResult<LikeOutput> AddLikeToRecipe(int recipeId, LikeInput likeInput)
        {
            try
            {
                _logger.LogInformation($"AddLikeToRecipe called: RecipeId - {recipeId}", recipeId);

                Recipe recipe = _recipeManager.GetRecipeById(recipeId);

                if (recipe == null)
                {
                    _logger.LogError($"Recipe with recipeId: {recipeId} could not be found!");
                    return NotFound($"Recipe with recipeId: {recipeId} could not be found!");
                }
                else
                {
                    DateTime likeDate = DateTime.Now;
                    Like like = new Like(likeDate);

                    _likeManager.AddLikeToRecipe(recipeId, like);

                    _logger.LogInformation($"Like added successfully: {like.LikeDate}", like.LikeDate);
                    return Ok(like);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error adding like: {ex.Message}", ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Recipe/{recipeId}/Like")]
        public ActionResult<List<Like>> GetLikesByRecipeId(int recipeId)
        {
            try
            {
                _logger.LogInformation($"GetLikesByRecipeId called: RecipeId - {recipeId}", recipeId);

                Recipe recipe = _recipeManager.GetRecipeById(recipeId);
                if (recipe == null)
                {
                    _logger.LogError($"Recipe with recipeId: {recipeId} could not be found!");
                    return NotFound($"Recipe with recipeId: {recipeId} could not be found!");
                }

                List<Like> likes = _likeManager.GetLikesByRecipeId(recipeId);
                if (likes.Count == 0)
                {
                    _logger.LogError($"No likes found on recipe with recipeId: {recipeId}");
                    return NotFound($"No likes found on recipe with recipeId: {recipeId}");
                }
                else
                {
                    _logger.LogInformation($"Retrieved likes for recipe ID: {recipeId}", recipeId);
                    return Ok(likes);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving likes for recipe ID: {recipeId}", recipeId);
                return BadRequest(ex.Message);
            }
        }
    }
}