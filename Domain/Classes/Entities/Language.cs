namespace Domain.Classes.Entities
{
    public sealed class Language
    {
        public int Id { get; }
        public string Name { get; }
        public string Icon { get; }
        public string SkillLevel { get; }

        public Language(int id, string name, string icon, string skillLevel)
        {
            if (string.IsNullOrWhiteSpace(name)) 
            {
                throw new ArgumentException("Language name is required.");
            }

            Id = id;
            Name = name.Trim();
            Icon = icon ?? string.Empty;
            SkillLevel = skillLevel ?? string.Empty;
        }
    }
}
