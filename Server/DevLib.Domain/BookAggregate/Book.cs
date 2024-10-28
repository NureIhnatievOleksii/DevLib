namespace DevLib.Domain.BookAggregate
{
    public class Book
    {
        public Guid BookId { get; set; }
        public string BookName { get; set; }
        public string Author { get; set; }
        public string FilePath { get; set; }
        public DateTime PublicationDateTime { get; set; }
    }
}