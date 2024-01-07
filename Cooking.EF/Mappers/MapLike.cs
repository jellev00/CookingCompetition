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
    public class MapLike
    {
        public static LikeEF MapToDB(Like like)
        {
            try
            {
                return new LikeEF(like.LikeId, like.LikeDate);
            }
            catch (Exception ex)
            {
                throw new MapException("MapLike - MapToDB", ex);
            }
        }

        public static Like MapToDomain(LikeEF likeEF)
        {
            try
            {
                return new Like(likeEF.LikeId, likeEF.LikeDate);
            }
            catch (Exception ex)
            {
                throw new MapException("MapLike - MapToDomain", ex);
            }
        }
    }
}