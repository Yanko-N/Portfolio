namespace Persistence.Models
{
    public class Skill
    {
        public int Id { get; set; }
        public string Icon { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public string? Motivation { get; set; }

        public int CategoryId { get; set; }
        public virtual SkillCategory? Category { get; set; }

    }
}
