using Domain.Classes.Values;

namespace Domain.Classes.Entities
{
    public sealed class Enterprise
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public Url Url { get; set; }
        public Image? Image { get; set; } 

        public Enterprise(int id, string name, string url, Image? image)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Enterprise name is required.", nameof(name));
            }
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentException("Enterprise URL is required.", nameof(url));
            }
            Url uri;
            try
            {
                uri = new Url(url);
            }
            catch (Exception e)
            {
                throw new ArgumentException("Invalid URL format.", nameof(url), e);
            }

            Id = id;
            Name = name.Trim();
            Url = uri;
            Image = image;
        }
    }
}
