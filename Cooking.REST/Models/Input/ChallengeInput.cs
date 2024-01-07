using Cooking.EF.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Cooking.REST.Models.Input
{
    public class ChallengeInput
    {
        public ChallengeInput(string challengeName, string challengeDescription, DateTime startDate, DateTime endDate)
        {
            ChallengeName = challengeName;
            ChallengeDescription = challengeDescription;
            StartDate = startDate;
            EndDate = endDate;
        }

        public string ChallengeName { get; set; }
        public string ChallengeDescription { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
