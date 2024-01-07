using Cooking.BL.Interfaces;
using Cooking.BL.Models;
using Cooking.EF.Exceptions;
using Cooking.EF.Mappers;
using Cooking.EF.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cooking.EF.Repositories
{
    public class UserRepositoryEF : IUserRepository
    {
        private readonly CookingContext ctx;

        public UserRepositoryEF(string connectionString)
        {
            this.ctx = new CookingContext(connectionString);
        }

        public User GetUserByEmail(string email)
        {
            try
            {
                return MapUser.MapToDomain(ctx.Users
                    .Include(x => x.Recipes)
                    .ThenInclude(x => x.Likes)
                    .Include(x => x.Recipes)
                    .ThenInclude(x => x.Images)
                    .AsNoTracking()
                    .FirstOrDefault(x => x.Email == email));

            }
            catch (Exception ex)
            {
                throw new UserRepositoryException("GetUserById", ex);
            }
        }

        public void AddUser(User user)
        {
            try
            {
                UserEF userEF = MapUser.MapToDB(user, ctx);
                ctx.Users.Add(userEF);
                SaveAndClear();
            }
            catch (Exception ex)
            {
                throw new UserRepositoryException("AddUser", ex);
            }
        }

        public bool UserExists(string email)
        {
            try
            {
                return ctx.Users.Any(x => x.Email == email);
            }
            catch (Exception ex)
            {
                throw new UserRepositoryException("UserExists", ex);
            }
        }

        private void SaveAndClear()
        {
            ctx.SaveChanges();
            ctx.ChangeTracker.Clear();
        }

        public User GetUserById(string email)
        {
            throw new NotImplementedException();
        }
    }
}