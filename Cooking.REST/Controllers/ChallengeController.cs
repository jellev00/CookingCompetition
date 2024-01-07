using Cooking.BL.Managers;
using Cooking.BL.Models;
using Cooking.REST.Models.Input;
using Cooking.REST.Models.Output;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Cooking.REST.Controllers
{
    [EnableCors("AllowSpecificOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class ChallengeController : ControllerBase
    {
        private readonly ChallengeManager _challengeManager;
        private readonly ILogger _logger;

        public ChallengeController(ChallengeManager challengeManager, ILoggerFactory loggerFactory)
        {
            _challengeManager = challengeManager;
            _logger = loggerFactory.AddFile("Logs/ChallengeConLogs.txt").CreateLogger("Cooking - Challenge");
        }

        [HttpPost]
        public ActionResult<ChallengeOutput> AddChallenge([FromBody] ChallengeInput challengeInput)
        {
            try
            {
                _logger.LogInformation("AddChallenge called: {ChallengeName}", challengeInput.ChallengeName);

                if (challengeInput.StartDate.Date < DateTime.Now.Date)
                {
                    _logger.LogError($"Challenge not added! Startdate: {challengeInput.StartDate} is in the past");
                    return BadRequest("StartDate can't be in the past!");
                }

                Challenge newChallenge = new Challenge(challengeInput.ChallengeName, challengeInput.ChallengeDescription, challengeInput.StartDate, challengeInput.EndDate, null);
                _challengeManager.AddChallenge(newChallenge);
                _logger.LogInformation($"Challenge added successfully: {newChallenge.ChallengeName}");

                Challenge challenge = new Challenge
                    (
                        newChallenge.ChallengeId,
                        challengeInput.ChallengeName,
                        challengeInput.ChallengeDescription,
                        challengeInput.StartDate,
                        challengeInput.EndDate,
                        null
                    );

                return CreatedAtAction(nameof(GetChallengeById), new { challengeId = newChallenge.ChallengeId }, challenge);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error adding challenge: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult<List<Challenge>> GetAllChallenges()
        {
            try
            {
                _logger.LogInformation("GetAllChallenges called");
                List<Challenge> challenges = _challengeManager.GetAllChallenges();

                if (challenges.Count == 0)
                {
                    return NotFound("No challenges found!");
                    _logger.LogError("Error retrieving challenges: no challenges found");
                }
                else
                {
                    return Ok(challenges);
                    _logger.LogInformation($"Retrieved {challenges.Count} challenges", challenges.Count);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving challenges: {ex.Message}", ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{challengeId}")]
        public ActionResult<Challenge> GetChallengeById(int challengeId)
        {
            try
            {
                _logger.LogInformation("GetChallengeById called: Id - {ChallengeId}", challengeId);
                Challenge challenge = _challengeManager.GetChallengeById(challengeId);

                if (challenge == null)
                {
                    _logger.LogError($"No challenge found with challengeId: {challengeId}");
                    return NotFound($"No challenge found with challengeId: {challengeId}");
                }
                else
                {
                    _logger.LogInformation($"Retrieved challenge by ID: {challengeId} -- Challenge: {challenge.ChallengeName}", challengeId, challenge.ChallengeName);
                    return Ok(challenge);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving challenge by ID: Id -- {challengeId}", challengeId, ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}