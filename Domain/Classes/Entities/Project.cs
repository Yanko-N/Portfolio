using Domain.Classes.Values;

namespace Domain.Classes.Entities
{
    public sealed class Project
    {
        public int Id { get; }
        public string Name { get;  }
        public string Description { get;  }
        public Url? DemoUrl { get; }
        public Url? SourceCodeUrl { get; }

        public ProjectCategory Category { get; }

        public IReadOnlyCollection<Skill> Skills {  get; }
        public IReadOnlyCollection<Image> Images { get; }

        public Project(int id, string name, string description, string? demoUrl, string? sourceCodeUrl,ProjectCategory? category,
            IEnumerable<Skill> skills, IEnumerable<Image> images)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Project name is required.");

            }

            if (category == null)
            {
                throw new ArgumentNullException(nameof(category), "Project category is required.");
            }

            if (!string.IsNullOrWhiteSpace(sourceCodeUrl))
            {
                try
                {
                    SourceCodeUrl = new Url(sourceCodeUrl);
                }
                catch (Exception e)
                {
                    throw new ArgumentException("Invalid URL format.", nameof(sourceCodeUrl), e);
                }
            }
            else
            {
                SourceCodeUrl = null;
            }

            if (!string.IsNullOrWhiteSpace(demoUrl))
            {
                try
                {
                    DemoUrl = new Url(demoUrl);
                }
                catch (Exception e)
                {
                    throw new ArgumentException("Invalid URL format.", nameof(DemoUrl), e);
                }
            }
            else
            {
                DemoUrl = null;
            }

            Id = id;
            Name = name.Trim();
            Description = description ?? string.Empty;
            Skills = skills?.ToList() ?? new List<Skill>();
            Images = images?.ToList() ?? new List<Image>();
            Category = category;

           
        }
    }
}
