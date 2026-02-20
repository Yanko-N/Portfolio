using Domain.Classes.Values;

namespace Domain.Classes.Entities
{
    public sealed class Image
    {
        public int Id { get; }
        public Url? Url { get; }
        public string Path { get; }
        public string AltText { get; }

        public Image(int id, string? url, string path, string altText)
        {
            if (string.IsNullOrWhiteSpace(path) && string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentException("Image path or URL is required.");
            }

            Id = id;
            Path = path ?? string.Empty;
            AltText = altText ?? string.Empty;

            if(!string.IsNullOrWhiteSpace(url))
            {
                try
                {
                    Url = new Url(url);
                }
                catch (Exception e)
                {
                    throw new ArgumentException("Invalid URL format.", nameof(url), e);
                }
            }
            else
            {
                Url = null;
            }

        }
    }
}
