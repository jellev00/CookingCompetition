using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cooking.EF.Exceptions
{
    public class ChallengeRepositoryException : Exception
    {
        public ChallengeRepositoryException(string? message) : base(message)
        {
        }

        public ChallengeRepositoryException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}