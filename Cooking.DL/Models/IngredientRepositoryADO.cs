using Cooking.BL.Interfaces;
using Cooking.BL.Models;
using Cooking.DL.Exceptions;
using System.Data;
using System.Data.SqlClient;

namespace Cooking.DL.Models
{
    public class IngredientRepositoryADO : IIngredientRepository
    {
        private string _connectionString;

        public IngredientRepositoryADO(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Ingredient> GetAllIngredients()
        {
            List<Ingredient> ingredients = new List<Ingredient>();
            string sql = "SELECT * FROM Ingredients";
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
                        Ingredient ingredient = new Ingredient(
                            (int)reader["Ingredient_ID"],
                            (string)reader["Ingredient_Name"],
                            (string)reader["Description"],
                            (string)reader["Image"]);
                        ingredients.Add(ingredient);
                    }
                    reader.Close();
                    return ingredients;
                } 
                catch (Exception ex)
                {
                    throw new IngredientRepositoryException("GetAllIngredients", ex);
                }
            }
        }

        public void AddIngredient(Ingredient ingredient)
        {
            try
            {
                string sql = "INSERT INTO Ingredients(Ingredient_ID, Ingredient_Name, Description, Image) VALUES (@ingredientId, @ingredientName, @description, @imageUrl)";
                using (SqlConnection conn = new SqlConnection(_connectionString))
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    try
                    {
                        conn.Open();
                        cmd.CommandText = sql;
                        cmd.Parameters.AddWithValue("@ingredientId", ingredient.IngredientId);
                        cmd.Parameters.AddWithValue("@ingredientName", ingredient.IngredientName);
                        cmd.Parameters.AddWithValue("@description", ingredient.Description);
                        cmd.Parameters.AddWithValue("@imageUrl", ingredient.ImageUrl);
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw new IngredientRepositoryException("AddIngredient - SQL", ex);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new IngredientRepositoryException("AddIngredient - RepoADO", ex);
            }
        }

        public bool IngredientExists(int ingredientId)
        {
            string sql = "SELECT COUNT(*) FROM Ingredients WHERE Ingredient_ID=@ingredientId";
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                try
                {
                    conn.Open();
                    cmd.CommandText = sql;
                    cmd.Parameters.AddWithValue("@ingredientId", ingredientId);
                    int n = (int)cmd.ExecuteScalar();
                    if (n > 0)
                    {
                        return true;
                    } else
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    throw new IngredientRepositoryException("IngredientExists", ex);
                }
            }
        }

        public Ingredient GetIngredientById(int ingredientId)
        {
            string sql = "SELECT * FROM Ingredients WHERE Ingredient_ID=@ingredientId";
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                try
                {
                    conn.Open();
                    cmd.CommandText = sql;
                    cmd.Parameters.AddWithValue("@ingredientId", ingredientId);
                    IDataReader reader = cmd.ExecuteReader();
                    reader.Read();
                    Ingredient ingredient = new Ingredient(
                        (int)reader["Ingredient_ID"],
                        (string)reader["Ingredient_Name"],
                        (string)reader["Description"],
                        (string)reader["Image"]);
                    reader.Close();
                    return ingredient;
                }
                catch (Exception ex)
                {
                    throw new IngredientRepositoryException("GetIngredientById", ex);
                }
            }
        }
    }
}