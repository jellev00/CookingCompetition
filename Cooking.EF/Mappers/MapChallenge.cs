using Cooking.BL.Models;
using Cooking.EF.Exceptions;
using Cooking.EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cooking.EF.Mappers
{
    public class MapChallenge
    {
        public static ChallengeEF MapToDB(Challenge challenge, CookingContext ctx)
        {
            try
            {
                List<RecipeEF> recipesEF = new List<RecipeEF>();
                if (challenge.Recipes != null)
                {
                    foreach (Recipe recipe in challenge.Recipes)
                    {
                        //recipesEF.Add(MapRecipe.MapToDB(recipe, ctx));
                    }

                    return new ChallengeEF(challenge.ChallengeName, challenge.Description, challenge.StartDate, challenge.EndDate, recipesEF);
                }

                else
                {
                    return new ChallengeEF(challenge.ChallengeName, challenge.Description, challenge.StartDate, challenge.EndDate, null);
                }

            }
            catch (Exception ex)
            {
                throw new MapException("MapChallenge - MapToDB", ex);
            }
        }

        public static Challenge MapToDomain(ChallengeEF challengeEF)
        {
            try
            {
                List<Recipe> recipes = new List<Recipe>();

                if (challengeEF.Recipes != null)
                {
                    foreach (RecipeEF recipeEF in challengeEF.Recipes)
                    {
                        recipes.Add(MapRecipe.MapToDomain(recipeEF));
                    }

                    return new Challenge(challengeEF.ChallengeId, challengeEF.ChallengeName, challengeEF.ChallengeDescription, challengeEF.StartDate, challengeEF.EndDate, recipes);

                }
                else
                {
                    return new Challenge(challengeEF.ChallengeId, challengeEF.ChallengeName, challengeEF.ChallengeDescription, challengeEF.StartDate, challengeEF.EndDate, null);
                }

            }
            catch (Exception ex)
            {
                throw new MapException("MapChallenge - MapToDomain", ex);
            }
        }
    }
}