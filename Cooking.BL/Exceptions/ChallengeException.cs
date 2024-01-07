namespace Cooking.BL.Exceptions
{
    public class ChallengeException : Exception
    {
        public ChallengeException(string? message) : base(message)
        {
        }

        public ChallengeException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}