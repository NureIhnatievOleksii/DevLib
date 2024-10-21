using DevLib.Domain.BookAggregate;
using DevLib.Domain.UserAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevLib.Domain.RatingAggregate
{
    public class Rating
    {
        public Guid RatingId { get; set; }
        public int PointsQuantity { get; set; }
        public Guid BookId { get; set; }
        public Guid UserId { get; set; }

        public Book Book { get; set; }
        public User User { get; set; }
    }
}