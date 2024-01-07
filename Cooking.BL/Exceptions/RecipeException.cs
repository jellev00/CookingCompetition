namespace Cooking.BL.Exceptions
{
    public class RecipeException : Exception
    {
        public RecipeException(string? message) : base(message)
        {
        }

        public RecipeException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}