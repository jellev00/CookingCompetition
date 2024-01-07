using Cooking.BL.Interfaces;
using Cooking.BL.Models;
using Cooking.EF.Exceptions;
using Cooking.EF.Mappers;
using Cooking.EF.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cooking.EF.Repositories
{
    public class ChallengeRepositoryEF : IChallengeRepository
    {
        private readonly CookingContext ctx;

        public ChallengeRepositoryEF(string connectionString)
        {
            this.ctx = new CookingContext(connectionString);
        }

        public void AddChallenge(Challenge challenge)
        {
            try
            {
                ChallengeEF challengeEF = MapChallenge.MapToDB(challenge, ctx);
                ctx.Challenges.Add(challengeEF);
                SaveAndClear();
                challenge.ChallengeId = challengeEF.ChallengeId;
            }
            catch (Exception ex)
            {
                throw new ChallengeRepositoryException("AddChallenge", ex);
            }
        }

        public List<Challenge> GetAllChallenges()
        {
            try
            {
                return ctx.Challenges.Select(x => MapChallenge.MapToDomain(x)).ToList();
            }
            catch (Exception ex)
            {
                throw new ChallengeRepositoryException("GetAllChallenges", ex);
            }
        }

        public Challenge GetChallengeById(int challengeId)
        {
            ChallengeEF challengeEF = ctx.Challenges.Where(x => x.ChallengeId == challengeId)
                .Include(x => x.Recipes)
                .ThenInclude(x => x.Likes)
                .Include(x => x.Recipes)
                .ThenInclude(x => x.User)
                .Include(x => x.Recipes)
                .ThenInclude(x => x.Images)
                .AsNoTracking()
                .FirstOrDefault();

            return MapChallenge.MapToDomain(challengeEF);
        }

        private void SaveAndClear()
        {
            ctx.SaveChanges();
            ctx.ChangeTracker.Clear();
        }
    }
}