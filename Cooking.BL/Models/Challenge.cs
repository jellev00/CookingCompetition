using Cooking.BL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cooking.BL.Models
{
    public class Challenge
    {
        public Challenge()
        {

        }

        public Challenge(int challengeId, string challengeName, string description, DateTime startDate, DateTime endDate, List<Recipe> recipes)
        {
            ChallengeId = challengeId;
            ChallengeName = challengeName;
            Description = description;
            StartDate = startDate;
            EndDate = endDate;
            Recipes = recipes;
        }

        public Challenge(string challengeName, string challengeDescription, DateTime startDate, DateTime endDate, List<Recipe> recipes)
        {
            ChallengeName = challengeName;
            Description= challengeDescription;
            StartDate = startDate;
            EndDate = endDate;
            Recipes = recipes;
        }

        private int _challengeId;
        public int ChallengeId
        {
            get
            {
                return _challengeId;
            }
            set
            {
                if (value <= 0)
                {
                    throw new ChallengeException("ID is invalid!");
                }
                else
                {
                    _challengeId = value;
                }
            }
        }

        private string _challengeName;
        public string ChallengeName
        {
            get
            {
                return _challengeName;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ChallengeException("ChallengeName is invalid!");
                }
                else
                {
                    _challengeName = value;
                }
            }
        }

        private string _description;
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ChallengeException("Description is invalid!");
                }
                else
                {
                    _description = value;
                }
            }
        }

        private DateTime _startDate;
        public DateTime StartDate
        {
            get
            {
                return _startDate;
            }
            set
            {
                _startDate = value;
            }
        }

        private DateTime _endDate;
        public DateTime EndDate
        {
            get
            {
                return _endDate;
            }
            set
            {
                if (value < StartDate)
                {
                    throw new ChallengeException("Date can't be in the past or earlier than the start date!");
                }
                else
                {
                    _endDate = value;
                }
            }
        }

        private List<Recipe> _recipes;
        public List<Recipe> Recipes
        {
            get
            {
                return _recipes;
            }
            set
            {
                _recipes = value;
            }
        }

        public void AddRecipe(Recipe recipe)
        {
            if (recipe == null)
            {
                throw new ChallengeException("AddRecipe - Recipe is null!");
            }
            if (_recipes.Contains(recipe))
            {
                throw new ChallengeException("AddRecipe - RecipeExists");
            }
            else
            {
                _recipes.Add(recipe);
            }
        }
    }
}