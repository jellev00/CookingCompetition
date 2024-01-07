namespace Cooking.BL.Exceptions
{
    public class ChallengeManagerException : Exception
    {
        public ChallengeManagerException(string? message) : base(message)
        {
        }

        public ChallengeManagerException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}