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
        public List<TagDto> Tags { get; set; }
    }

    public class ReviewDto
    {
        public string UserImg { get; set; }
        public string UserName { get; set; }
        public int Rate { get; set; }
        public DateTime CreationDate { get; set; }
        public string Text { get; set; }
    }
    public class TagDto
    {
        public Guid TagId { get; set; }
        public string TagText { get; set; }
    }
}