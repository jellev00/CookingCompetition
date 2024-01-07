using Cooking.BL.Exceptions;
using Cooking.BL.Interfaces;
using Cooking.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cooking.BL.Managers
{
    public class UserManager
    {
        private readonly IUserRepository _repo;

        public UserManager(IUserRepository repo)
        {
            _repo = repo;
        }

        public User GetUserByEmail(string email)
        {
            try
            {
                return _repo.GetUserByEmail(email);
            }
            catch (Exception ex)
            {
                throw new UserManagerException("GetUserById", ex);
            }
        }

        public void AddUser(User user)
        {
            try
            {
                _repo.AddUser(user);
            }
            catch (Exception ex)
            {
                throw new UserManagerException("AddUser", ex);
            }
        }

        public bool UserExists(string email)
        {
            try
            {
                return _repo.UserExists(email);
            }
            catch (Exception ex)
            {
                throw new UserManagerException("UserExists", ex);
            }
        }
    }
}