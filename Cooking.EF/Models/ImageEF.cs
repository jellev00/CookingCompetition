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
    public class ImageEF
    {
        public ImageEF()
        {

        }

        public ImageEF(string imageUrl)
        {
            ImageUrl = imageUrl;
        }

        // MapToDB

        public ImageEF(int imageId, string imageUrl)
        {
            ImageId = imageId;
            ImageUrl = imageUrl;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ImageId { get; set; }
        public string ImageUrl { get; set; }
        public RecipeEF Recipe { get; set; }
    }
}