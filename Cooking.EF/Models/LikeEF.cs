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
    public class LikeEF
    {
        public LikeEF()
        {

        }

        // MapToDB
        public LikeEF(DateTime likeDate)
        {
            LikeDate = likeDate;
        }

        public LikeEF(int likeId, DateTime likeDate)
        {
            LikeId = likeId;
            LikeDate = likeDate;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LikeId { get; set; }
        public DateTime LikeDate { get; set; }
        public RecipeEF Recipe { get; set; }
    }
}