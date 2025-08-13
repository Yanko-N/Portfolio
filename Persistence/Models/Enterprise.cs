namespace Persistence.Models
{
    public class Enterprise
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public string Url { get; set; }
        public int? ImageId { get; set; }
        public virtual Image? Image { get; set; } 
    }
}
