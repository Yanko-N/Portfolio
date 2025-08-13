using Domain.Classes.Values;
using Domain.Extensions;

namespace Domain.Classes.Entities
{
    public sealed class Social
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Url Url { get; set; }
        public string Icon { get; set; }

        public Social(int id, string name, string url, string icon)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Social name is required.", nameof(name));
            }


            if (string.IsNullOrWhiteSpace(icon))
            {
                throw new ArgumentException("Social icon is required.", nameof(icon));
            }
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentException("URL is required.", nameof(url));
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
            Icon = icon.ResolveMudBlazorIcon();
            Url = uri;
        }
    }
}
