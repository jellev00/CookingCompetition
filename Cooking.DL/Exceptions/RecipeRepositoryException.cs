using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cooking.DL.Exceptions
{
    public class RecipeRepositoryException : Exception
    {
        public RecipeRepositoryException(string? message) : base(message)
        {
        }

        public RecipeRepositoryException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
