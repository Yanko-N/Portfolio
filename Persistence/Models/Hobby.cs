namespace Persistence.Models
{
    public class Hobby
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public int? ImageId { get; set; }
        public virtual Image? Image { get; set; }

        public string Icon { get; set; }
    }
}
