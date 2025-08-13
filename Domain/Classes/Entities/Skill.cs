using Domain.Extensions;

namespace Domain.Classes.Entities
{
    public sealed class Skill
    {
        public int Id { get; }
        public string Icon { get; }
        public string Description { get; }
        public string Name { get; }
        public string? Motivation { get; }
        public SkillCategory Category { get; }

        public Skill(int id, string icon, string description, string name, string? motivation, SkillCategory category)
        {
            if (string.IsNullOrWhiteSpace(name)) 
            { 
                throw new ArgumentException("Skill name is required."); 
            }

            if (category == null) 
            { 
                throw new ArgumentNullException(nameof(category), "Skill category is required.");
            }

            Id = id;

            Icon = string.IsNullOrWhiteSpace(icon) ? string.Empty : icon.ResolveMudBlazorIcon(); 
            Description = description ?? string.Empty;
            Name = name.Trim();
            Motivation = motivation;
            Category = category;
        }
    }
}
