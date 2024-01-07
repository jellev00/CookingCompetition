using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cooking.BL.Exceptions
{
    public class LikeException : Exception
    {
        public LikeException(string? message) : base(message)
        {
        }

        public LikeException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}