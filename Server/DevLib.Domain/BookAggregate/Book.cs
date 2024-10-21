using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevLib.Domain.BookAggregate
{
    public class Book
    {
        public Guid BookId { get; set; }
        public string BookName { get; set; }
        public string Author { get; set; }
        public string FilePath { get; set; }
    }
}