using Cooking.BL.Managers;
using Cooking.BL.Models;
using Cooking.EF.Exceptions;
using Cooking.EF.Mappers;
using Cooking.EF.Models;
using Cooking.REST.Models.Input;
using Cooking.REST.Models.Output;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Cooking.REST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly ImageManager _imageManager;
        private readonly RecipeManager _recipeManager;
        private readonly ILogger _logger;

        public ImageController(ImageManager imageManager, RecipeManager recipeManager, ILoggerFactory loggerFactory)
        {
            _imageManager = imageManager;
            _recipeManager = recipeManager;
            _logger = loggerFactory.AddFile("Logs/ImageConLogs.txt").CreateLogger("Cooking - Image");
        }

        [HttpPost("~/api/recipe/{recipeId}/[controller]/upload")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> AddImage(IFormFile image, int recipeId)
        {
            try
            {
                _logger.LogInformation($"AddImage called: RecipeId - {recipeId}", recipeId);

                if (_recipeManager.GetRecipeById(recipeId) == null)
                {
                    _logger.LogError($"Recipe with recipeId: {recipeId} could not be found!");
                    return NotFound($"Recipe with recipeId: {recipeId} could not be found!");
                }
                else
                {
                    Image newImage = new Image("");

                    var imagePath = Path.Combine("..", "cooking.ui", "src", "assets", "Images", Guid.NewGuid().ToString() + Path.GetExtension(image.FileName));

                    newImage.ImageUrl = Path.Combine("Images", Path.GetFileName(imagePath).Replace("\\", "/"));

                    _imageManager.AddImagesToRecipe(recipeId, newImage);

                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        await image.CopyToAsync(stream);
                    }

                    var secondImagePath = Path.Combine("..", "jury.ui", "src", "assets", newImage.ImageUrl);

                    using (var secondStream = new FileStream(secondImagePath, FileMode.Create))
                    {
                        await image.CopyToAsync(secondStream);
                    }

                    _logger.LogInformation($"Image uploaded successfully for recipe ID: {recipeId}", recipeId);
                    return Ok("Image uploaded successfully");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while uploading image for recipe ID: {recipeId}", recipeId);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("~/api/Recipe/{recipeId}/[controller]")]
        public ActionResult<List<Image>> GetImageByRecipeId(int recipeId)
        {
            try
            {
                _logger.LogInformation("GetImageByRecipeId called: Id - {RecipeId}", recipeId);

                Recipe recipe = _recipeManager.GetRecipeById(recipeId);
                List<Image> images = _imageManager.GetImagesFromRecipe(recipeId);

                if (recipe == null)
                {
                    _logger.LogError($"Recipe with recipeId: {recipeId} could not be found!");
                    return NotFound($"Recipe with recipeId: {recipeId} could not be found!");
                }
                else if (images == null)
                {
                    _logger.LogError($"No images found on recipe with recipeId: {recipeId}");
                    return NotFound($"No images found on recipe with recipeId: {recipeId}");
                }
                else
                {
                    _logger.LogInformation($"Retrieved {images.Count} images", images.Count);
                    return Ok(images);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while retrieving images for recipe ID: {recipeId}", recipeId);
                return BadRequest(ex.Message);
            }
        }
    }
}