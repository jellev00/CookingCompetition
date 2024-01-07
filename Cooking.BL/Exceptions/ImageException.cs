using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cooking.BL.Exceptions
{
    public class ImageException : Exception
    {
        public ImageException(string? message) : base(message)
        {
        }

        public ImageException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}