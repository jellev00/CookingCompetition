using Cooking.BL.Models;

namespace Cooking.BL.Interfaces
{
    public interface IChallengeRepository
    {
        void AddChallenge(Challenge challenge);
        List<Challenge> GetAllChallenges();
        Challenge GetChallengeById(int challengeId);
    }
}