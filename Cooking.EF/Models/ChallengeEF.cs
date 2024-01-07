using Cooking.BL.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cooking.EF.Models
{
    public class ChallengeEF
    {
        public ChallengeEF()
        {

        }

        public ChallengeEF(string challengeName, string description, DateTime startDate, DateTime endDate, List<RecipeEF> recipes)
        {
            ChallengeName = challengeName;
            ChallengeDescription = description;
            StartDate = startDate;
            EndDate = endDate;
            Recipes = recipes;
        }

        public ChallengeEF(int challengeId, string challengeName, string challengeDescription, DateTime startDate, DateTime endDate, List<RecipeEF> recipes)
        {
            ChallengeId = challengeId;
            ChallengeName = challengeName;
            ChallengeDescription = challengeDescription;
            StartDate = startDate;
            EndDate = endDate;
            Recipes = recipes;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ChallengeId { get; set; }
        public string ChallengeName { get; set; }
        public string ChallengeDescription { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<RecipeEF> Recipes { get; set; } = new List<RecipeEF>();
    }
}