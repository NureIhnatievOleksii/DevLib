namespace DevLib.Application.CQRS.Dtos.Queries
{
    public class GetBookByIdQueryDto
    {
        public Guid BookId { get; set; }
        public string BookName { get; set; }
        public string BookImg { get; set; }
        public string PDF { get; set; }
        public string Author { get; set; }
        public double AverageRating { get; set; }
        public List<ReviewDto> Reviews { get; set; } = new();
    }

    public class ReviewDto
    {
        public string UserImg { get; set; }
        public int Rate { get; set; }
        public DateTime CreationDate { get; set; }
        public string Text { get; set; }
    }
}