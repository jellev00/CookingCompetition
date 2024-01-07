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
    public class UserController : ControllerBase
    {
        private UserManager _userManager;
        private readonly ILogger _logger;

        public UserController(UserManager userManager, ILoggerFactory loggerFactory)
        {
            _userManager = userManager;
            _logger = loggerFactory.AddFile("Logs/UserConLogs.txt").CreateLogger("Cooking - User");
        }

        [HttpPost]
        public ActionResult<UserOutput> AddUser([FromBody] UserInput userInput)
        {
            try
            {
                User user = new User(userInput.Email);

                if (_userManager.UserExists(user.Email))
                {
                    return BadRequest($"User with email: {user.Email} already exists!");
                }
                else
                {
                    _userManager.AddUser(user);
                    return Ok(user);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{email}")]
        public ActionResult<User> GetUserByEmail(string email)
        {
            try
            {
                _logger.LogInformation($"GetUserByEmail called: Email - {email}", email);

                if (!_userManager.UserExists(email))
                {
                    _logger.LogError($"User with email: {email} could not be found!");
                    return NotFound($"User with email: {email} could not be found!");
                }
                else
                {
                    _logger.LogInformation($"Retrieved user: {email}");
                    User user = _userManager.GetUserByEmail(email);
                    return Ok(user);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving user: {ex.Message}", ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}