using Cooking.BL.Interfaces;
using Cooking.BL.Models;
using Cooking.DL.Exceptions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cooking.BL.Managers;

namespace Cooking.DL.Models
{
    public class UserRepositoryADO : IUserRepository
    {
        private string _connectionString;

        public UserRepositoryADO(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void AddUser(User user)
        {
            string sql = "INSERT INTO Users(User_ID, Email) VALUES (@userid, @email)";
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                try
                {
                    conn.Open();
                    cmd.CommandText = sql;
                    cmd.Parameters.AddWithValue("@userId", user.UserId);
                    cmd.Parameters.AddWithValue("@email", user.Email);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new IngredientRepositoryException("AddIngredient", ex);
                }
            }
        }

        public List<User> GetAllUsers()
        {
            List<User> users = new List<User>();
            string sql = "SELECT * FROM Users";
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                try
                {
                    conn.Open();
                    cmd.CommandText = sql;
                    IDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        User user = new User(
                        (int)reader["User_ID"],
                        (string)reader["Email"]);
                        users.Add(user);
                    }
                    reader.Close();
                    return users;
                }
                catch (Exception ex)
                {
                    throw new UserRepositoryException("GetAllUsers", ex);
                }
            }
        }

        public User GetUserById(int id)
        {
            string sql = "SELECT * FROM Users WHERE User_ID=@userId";
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                try
                {
                    conn.Open();
                    cmd.CommandText = sql;
                    cmd.Parameters.AddWithValue("@userId", id);
                    IDataReader reader = cmd.ExecuteReader();
                    reader.Read();
                    User user = new User(
                        (int)reader["User_ID"],
                        (string)reader["Email"]);
                    reader.Close();
                    return user;
                }
                catch (Exception ex)
                {
                    throw new UserRepositoryException("GetUserById", ex);
                }
            }
        }
    }
}