using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cooking.EF.Exceptions
{
    public class ImageRepositoryException : Exception
    {
        public ImageRepositoryException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}