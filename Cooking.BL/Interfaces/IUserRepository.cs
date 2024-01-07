using Cooking.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cooking.BL.Interfaces
{
    public interface IUserRepository
    {
        User GetUserByEmail(string email);
        void AddUser(User user);
        bool UserExists(string email);
    }
}