namespace api.Entities
{
    public class MainContent
    {
        [Key]
        public int Id { get; set; }
        public string? HeaderContent { get; set; }
        public string? MenuButtonText { get; set; }
    }
}