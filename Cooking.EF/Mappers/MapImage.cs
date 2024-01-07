using Cooking.BL.Models;
using Cooking.EF.Exceptions;
using Cooking.EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cooking.EF.Mappers
{
    public class MapImage
    {
        public static ImageEF MapToDB(Image image)
        {
            try
            {
                return new ImageEF(image.ImageId, image.ImageUrl);
            }
            catch (Exception ex)
            {
                throw new MapException("MapImage - MapToDB", ex);
            }
        }

        public static Image MapToDomain(ImageEF imageEF)
        {
            try
            {
                return new Image(imageEF.ImageId, imageEF.ImageUrl);
            }
            catch (Exception ex)
            {
                throw new MapException("MapImage - MapToDomain", ex);
            }
        }
    }
}