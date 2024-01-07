using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cooking.BL.Managers
{
    public class UserRepositoryException : Exception
    {
        public UserRepositoryException(string? message) : base(message)
        {
        }

        public UserRepositoryException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}