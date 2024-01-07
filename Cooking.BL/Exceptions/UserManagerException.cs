using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cooking.BL.Exceptions
{
    public class UserManagerException : Exception
    {
        public UserManagerException(string? message) : base(message)
        {
        }

        public UserManagerException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
