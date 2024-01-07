namespace Cooking.DL.Exceptions
{
    public class ChallengeRepositoryException : Exception
    {
        public ChallengeRepositoryException(string? message) : base(message)
        {
        }

        public ChallengeRepositoryException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}