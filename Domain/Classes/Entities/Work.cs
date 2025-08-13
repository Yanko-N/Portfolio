using Domain.Classes.Values;

namespace Domain.Classes.Entities
{
    public sealed class Work
    {
        public int Id { get; }
        public Enterprise Enterprise { get; }
        public DateRange Period { get; }
        public string Title { get; }
        public string Description { get; }
        public IReadOnlyCollection<Skill> Skills { get; }

        public Work(int id, Enterprise enterprise, DateRange period, string title, string description, IEnumerable<Skill> skills)
        {
            if (enterprise == null)
            {
                    throw new ArgumentException("EnterpriseId must be positive.");
            }

            if (string.IsNullOrWhiteSpace(title)) 
            { 
                throw new ArgumentException("Work title is required.");
            }

            if(period == null) 
            { 
                throw new ArgumentNullException(nameof(period), "Period is required.");
            }
            
            Id = id;
            Enterprise = enterprise;
            Period = period;
            Title = title.Trim();
            Description = description ?? string.Empty;
            Skills = skills.ToList() ?? new List<Skill>();
        }
    }
}
