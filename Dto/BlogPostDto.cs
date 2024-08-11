namespace api.Dto
{
    public class BlogPostDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime PublishDate { get; set; }
    }
}