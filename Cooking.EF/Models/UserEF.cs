using Cooking.BL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cooking.EF.Models
{
    public class UserEF
    {
        public UserEF()
        {

        }

        public UserEF(string email)
        {
            Email = email;
        }

        public UserEF(string email, List<RecipeEF> recipes)
        {
            Email = email;
            Recipes = recipes;
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column(TypeName = "varchar(250)")]
        public string Email { get; set; }
        public List<RecipeEF> Recipes { get; set; } = new List<RecipeEF>();
    }
}