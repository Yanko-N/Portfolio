namespace Persistence.Models
{
    public class Image
    {
        public int Id { get; set; }
        public string? Url { get; set; }
        public string? Path { get; set; }

        public string? AltText { get; set; }
    }
}
