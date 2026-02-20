namespace Domain.Classes.Entities
{
    public sealed class SkillCategory
    {
        public int Id { get; }
        public string Name { get; }

        public SkillCategory(int id, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Skill category name is required.");
            }

            Id = id;
            Name = name.Trim();
        }
    }
}
