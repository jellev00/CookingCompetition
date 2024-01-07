using Cooking.BL.Interfaces;
using Cooking.BL.Managers;
using Cooking.EF.Models;
using Cooking.EF.Repositories;

namespace Cooking.REST
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=CookingCompetitionTestDB;Integrated Security=True";
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddSingleton<IChallengeRepository>(r => new ChallengeRepositoryEF(connectionString));
            builder.Services.AddSingleton<ChallengeManager>();
            builder.Services.AddSingleton<IRecipeRepository>(r => new RecipeRepositoryEF(connectionString));
            builder.Services.AddSingleton<RecipeManager>();
            builder.Services.AddSingleton<IUserRepository>(r => new UserRepositoryEF(connectionString));
            builder.Services.AddSingleton<UserManager>();
            builder.Services.AddSingleton<ILikeRepository>(r => new LikeRepositoryEF(connectionString));
            builder.Services.AddSingleton<LikeManager>();
            builder.Services.AddSingleton<IImageRepository>(r => new ImageRepositoryEF(connectionString));
            builder.Services.AddSingleton<ImageManager>();
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Create a CORS policy with your desired configuration.
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:5173", "http://localhost:5174") // Replace with your frontend's URL
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();

            // Enable CORS by applying the policy.
            app.UseCors("AllowSpecificOrigin");

            app.MapControllers();

            app.Run();
        }
    }
}
