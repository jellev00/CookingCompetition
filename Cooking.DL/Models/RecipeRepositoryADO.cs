using Cooking.BL.Models;
using System.Data.SqlClient;
using System.Data;
using Cooking.DL.Exceptions;
using Cooking.BL.Interfaces;

namespace Cooking.DL.Models
{
    public class RecipeRepositoryADO : IRecipeRepository
    {
        private string _connectionString;

        public RecipeRepositoryADO(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Recipe GetRecipeById(int recipeId)
        {
            string sql = "SELECT * FROM Recipes WHERE Recipe_ID=@recipeId";
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                try
                {
                    conn.Open();
                    cmd.CommandText = sql;
                    cmd.Parameters.AddWithValue("@recipeId", recipeId);
                    IDataReader reader = cmd.ExecuteReader();
                    reader.Read();
                    Recipe recipe = new Recipe(
                       (int)reader["Recipe_ID"],
                       (string)reader["Recipe_Name"],
                       (string)reader["Description"],
                       (int)reader["Image_ID"],
                       (int)reader["User_ID"]);
                    reader.Close();
                    return recipe;
                } catch (Exception ex)
                {
                    throw new RecipeRepositoryException("GetRecipeById", ex);
                }
            }
        }

        public List<Recipe> GetAllRecipes()
        {
            List<Recipe> recipes = new List<Recipe>();
            string sql = "SELECT * FROM Recipes";
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
                        Recipe recipe = new Recipe(
                        (int)reader["Recipe_ID"],
                        (string)reader["Recipe_Name"],
                        (string)reader["Description"],
                        (int)reader["Image_ID"],
                        (int)reader["User_ID"]);
                        recipes.Add(recipe);
                    }
                    reader.Close();
                    return recipes;
                } catch (Exception ex)
                {
                    throw new RecipeRepositoryException("GetAllRecipes", ex);
                }
            }
        }

        public List<Recipe> GetRecipesByChallengeId(int challengeId)
        {
            List<Recipe> recipes = new List<Recipe>();
            string sql = "SELECT R.Recipe_ID, R.User_ID, R.Recipe_Name, R.Description, R.Image_ID FROM User_Challenge_Recipes UCR INNER JOIN Challenges C ON UCR.Challenge_ID = C.Challenge_ID INNER JOIN Recipes R ON UCR.Recipe_ID = R.Recipe_ID INNER JOIN Users U ON R.User_ID = U.User_ID WHERE C.Challenge_ID = @challengeId";
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                try
                {
                    conn.Open();
                    cmd.CommandText = sql;
                    cmd.Parameters.AddWithValue("@challengeId", challengeId);
                    IDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Recipe recipe = new Recipe(
                         (int)reader["Recipe_ID"],
                         (string)reader["Recipe_Name"],
                         (string)reader["Description"],
                         (int)reader["Image_ID"],
                         (int)reader["User_ID"]);
                        recipes.Add(recipe);
                    }
                    reader.Close();
                    return recipes;
                } catch (Exception ex)
                {
                    throw new RecipeRepositoryException("GetRecipesByChallengeId", ex);
                }
            }
        }

        public void AddRecipe(Recipe recipe)
        {
            string sql = "INSERT INTO Recipes (Recipe_ID, User_ID, Recipe_Name, Description, Image_ID) VALUES (@recipeId, @userId, @recipeName, @description, @imageId)";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                try
                {
                    conn.Open();
                    cmd.CommandText = sql;
                    cmd.Parameters.AddWithValue("@recipeId", recipe.RecipeId);
                    cmd.Parameters.AddWithValue("@userId", recipe.UserId);
                    cmd.Parameters.AddWithValue("@recipeName", recipe.RecipeName);
                    cmd.Parameters.AddWithValue("@description", recipe.Description);
                    cmd.Parameters.AddWithValue("@imageId", recipe.ImageId);
                    cmd.ExecuteNonQuery();
                } catch (Exception ex)
                {
                    throw new RecipeRepositoryException("AddRecipe", ex);
                }
            }
        }
    }
}