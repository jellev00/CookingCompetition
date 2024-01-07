using Cooking.BL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cooking.BL.Models
{
    public class Like
    {
        public Like(int likeId, DateTime likeDate)
        {
            LikeId = likeId;
            LikeDate = likeDate;
        }

        public Like(DateTime likeDate)
        {
            LikeDate = likeDate;
        }

        private int _likeId;
        public int LikeId
        {
            get
            {
                return _likeId;
            }
            set
            {
                if (value <= 0)
                {
                    throw new LikeException("LikeId can't be less then 1");
                }
                else
                {
                    _likeId = value;
                }
            }
        }

        private DateTime _likeDate;
        public DateTime LikeDate
        {
            get
            {
                return _likeDate;
            }
            private set
            {
                _likeDate = DateTime.Now;
            }
        }
    }
}