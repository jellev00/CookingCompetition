using Cooking.BL.Exceptions;
using Cooking.BL.Interfaces;
using Cooking.BL.Models;

namespace Cooking.BL.Managers
{
    public class ChallengeManager
    {
        private readonly IChallengeRepository _repo;

        public ChallengeManager(IChallengeRepository repo)
        {
            _repo = repo;
        }

        public List<Challenge> GetAllChallenges()
        {
            try
            {
                return _repo.GetAllChallenges();
            } 
            catch (Exception ex)
            {
                throw new ChallengeManagerException("GetAllChallenges", ex);
            }
        }

        public Challenge GetChallengeById(int challengeId)
        {
            try
            {
                return _repo.GetChallengeById(challengeId);
            }
            catch (Exception ex)
            {
                throw new ChallengeManagerException("GetChallengeById", ex);
            }
        }

        public void AddChallenge(Challenge challenge)
        {
            try
            {
                _repo.AddChallenge(challenge);
            }
            catch (Exception ex)
            {
                throw new ChallengeManagerException("AddChallenge", ex);
            }
        }
    }
}
