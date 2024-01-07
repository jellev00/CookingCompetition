using Cooking.EF.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Cooking.BL.Models;

namespace Cooking.REST.Models.Output
{
    public class ChallengeOutput
    {
        public ChallengeOutput(int challengeId, string challengeName, string challengeDescription, DateTime startDate, DateTime endDate, List<Recipe> recipes)
        {
            ChallengeId = challengeId;
            ChallengeName = challengeName;
            ChallengeDescription = challengeDescription;
            StartDate = startDate;
            EndDate = endDate;
            Recipes = recipes;
        }

        public int ChallengeId { get; set; }
        public string ChallengeName { get; set; }
        public string ChallengeDescription { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<Recipe> Recipes { get; set; }
    }
}
