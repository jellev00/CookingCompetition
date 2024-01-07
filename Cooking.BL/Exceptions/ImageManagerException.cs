using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cooking.BL.Exceptions
{
    public class ImageManagerException : Exception
    {
        public ImageManagerException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}