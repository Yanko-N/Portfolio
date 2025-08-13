namespace Domain.Classes.Entities
{
    public sealed class Course
    {
        public int Id { get; }
        public string Name { get; }
        public string Description { get; }
        public Uri Url { get; }

        public Course(int id, string name, string description, string url)
        {
            if (string.IsNullOrWhiteSpace(name)) 
            { 
                throw new ArgumentException("Course name is required."); 
            }

            if(string.IsNullOrWhiteSpace(url)) 
            { 
                throw new ArgumentException("Course URL is required.", nameof(url));
            }
            Uri uri;
            try 
            { 
                 uri = new Uri(url);
            }
            catch (Exception e)
            {
                throw new ArgumentException("Invalid URL format.", nameof(url), e);
            }

            Id = id;
            Name = name.Trim();
            Description = description ?? string.Empty;
            Url = uri;
        }
    }
}
