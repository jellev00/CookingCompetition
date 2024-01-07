namespace Cooking.BL.Exceptions
{
    public class RecipeManagerException : Exception
    {
        public RecipeManagerException(string? message) : base(message)
        {
        }

        public RecipeManagerException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
