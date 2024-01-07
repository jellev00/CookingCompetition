using Cooking.BL.Exceptions;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cooking.BL.Models
{
    public class Image
    {
        public Image(int imageId, string imageUrl)
        {
            ImageId = imageId;
            ImageUrl = imageUrl;
        }

        public Image(string imageUrl)
        {
            ImageUrl = imageUrl;
        }

        private int _imageId;
        public int ImageId
        {
            get
            {
                return _imageId;
            }
            set
            {
                if (value <= 0)
                {
                    throw new ImageException("ImageId can't be smaller then 1!");
                }
                else
                {
                    _imageId = value;
                }
            }
        }

        private string _imageUrl;
        public string ImageUrl
        {
            get
            {
                return _imageUrl;
            }
            set
            {
                _imageUrl = value;
            }
        }
    }
}