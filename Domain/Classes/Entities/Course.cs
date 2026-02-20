using Domain.Classes.Values;

namespace Domain.Classes.Entities
{
    public sealed class Course
    {
        public int Id { get; }
        public string Name { get; }
        public string Description { get; }
        public Url Url { get; }

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
            Description = description ?? string.Empty;
            Url = uri;
        }
    }
}
