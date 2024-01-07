using Cooking.EF.Models;

namespace Testconsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=CookingCompetitionTestDB;Integrated Security=True;TrustServerCertificate=true";

            CookingContext ctx = new CookingContext(connectionString);

            ctx.Database.EnsureDeleted();
            ctx.Database.EnsureCreated();
        }
    }
}