using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cooking.BL.Exceptions
{
    public class LikeManagerException : Exception
    {
        public LikeManagerException(string? message) : base(message)
        {
        }

        public LikeManagerException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}