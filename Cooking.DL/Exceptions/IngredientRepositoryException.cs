namespace Cooking.DL.Exceptions
{
    public class IngredientRepositoryException : Exception
    {
        public IngredientRepositoryException(string? message) : base(message)
        {
        }

        public IngredientRepositoryException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
