using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cooking.EF.Models
{
    public class CookingContext : DbContext
    {
        private string _connectionString;

        public CookingContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public DbSet<ChallengeEF> Challenges { get; set; }
        public DbSet<ImageEF> Images { get; set; }
        public DbSet<LikeEF> Likes { get; set; }
        public DbSet<RecipeEF> Recipes { get; set; }
        public DbSet<UserEF> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString)
                .LogTo(Console.WriteLine, LogLevel.Information);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //// Relatie Challenge - Recipe
            //modelBuilder.Entity<ChallengeEF>()
            //    .HasMany(x => x.Recipes)
            //    .WithMany(x => x.Challenges)
            //    .UsingEntity(j => j.ToTable("ChallengeEFRecipeEF"));

            //// Relatie Recipe - User
            //modelBuilder.Entity<RecipeEF>()
            //    .HasOne(x => x.User)
            //    .WithMany(x => x.Recipes);

            //// Relatie Recipe - Like
            //modelBuilder.Entity<RecipeEF>()
            //    .HasMany(x => x.Likes)
            //    .WithOne(x => x.Recipe);

            //// Relatie Recipe - Image
            //modelBuilder.Entity<RecipeEF>()
            //    .HasMany(x => x.Images)
            //    .WithOne(x => x.Recipe);

            //// Relatie Recipe - Ingredient
            //modelBuilder.Entity<RecipeEF>()
            //    .HasMany(x => x.Ingredients)
            //    .WithMany(x => x.Recipes)
            //    .UsingEntity(j => j.ToTable("IngredientEFRecipeEF"));

            //modelBuilder.Entity<ChallengeEF>()
            //    .HasKey(c => c.ChallengeId);

            //// Ensure RecipeId is used as the primary key for ChallengeRecipe table
            //modelBuilder.Entity<RecipeEF>()
            //    .HasKey(r => r.RecipeId);
        }
    }
}