using Cooking.BL.Interfaces;
using Cooking.BL.Models;
using Cooking.DL.Exceptions;
using System.Data;
using System.Data.SqlClient;

namespace Cooking.DL.Models
{
    public class ChallengeRepositoryADO : IChallengeRepository
    {
        private string _connectionstring;

        public ChallengeRepositoryADO(string connectionstring)
        {
            _connectionstring = connectionstring;
        }

        public Challenge GetChallengeById(int challengeId)
        {
            string sql = "SELECT * FROM Challenges WHERE Challenge_ID=@challengeId";
            using (SqlConnection conn = new SqlConnection(_connectionstring))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                try
                {
                    conn.Open();
                    cmd.CommandText = sql;
                    cmd.Parameters.AddWithValue("@challengeId", challengeId);
                    IDataReader reader = cmd.ExecuteReader();
                    reader.Read();
                    Challenge challenge = new Challenge(
                        (int)reader["Challenge_ID"],
                        (string)reader["Challenge_Name"],
                        (string)reader["Description"],
                        (DateTime)reader["Start_Date"],
                        (DateTime)reader["End_Date"]);
                    reader.Close();
                    return challenge;
                } catch (Exception ex)
                {
                    throw new ChallengeRepositoryException("GetChallengeById", ex);
                }
            }
        }

        public List<Challenge> GetAllChallenges()
        {
            List<Challenge> challenges = new List<Challenge>();
            string sql = "SELECT * FROM Challenges";
            using (SqlConnection conn = new SqlConnection(_connectionstring))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                try
                {
                    conn.Open();
                    cmd.CommandText = sql;
                    IDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Challenge challenge = new Challenge(
                            (int)reader["Challenge_ID"],
                            (string)reader["Challenge_Name"],
                            (string)reader["Description"],
                            (DateTime)reader["Start_Date"],
                            (DateTime)reader["End_Date"]);

                        challenges.Add(challenge);
                    }
                    reader.Close();
                    return challenges;
                } catch (Exception ex)
                {
                    throw new ChallengeRepositoryException("GetAllChallenges", ex);
                }
            }
        }

        public void AddChallenge(Challenge challenge)
        {
            string sql = "INSERT INTO Challenges(Challenge_ID, Challenge_Name, Description, Start_Date, End_Date) VALUES (@challengeId, @challengeName, @description, @startDate, @endDate)";
            using (SqlConnection conn = new SqlConnection(_connectionstring))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                try
                {
                    conn.Open();
                    cmd.CommandText = sql;
                    cmd.Parameters.AddWithValue("@challengeId", challenge.ChallengeId);
                    cmd.Parameters.AddWithValue("@challengeName", challenge.ChallengeName);
                    cmd.Parameters.AddWithValue("@description", challenge.Description);
                    cmd.Parameters.AddWithValue("@startDate", challenge.StartDate);
                    cmd.Parameters.AddWithValue("@endDate", challenge.EndDate);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new ChallengeRepositoryException("AddChallenge", ex);
                }
            }
        }
    }
}

// ADDEN: 
// tussentabel tussen Challenges en Recipes

// Challenges: Id, Name, Descr, Start, End
// Tussentabel: Id, Challenge_Id, Recipe_Id
// Recipes: Id, User_Id, Name, Desc, Image