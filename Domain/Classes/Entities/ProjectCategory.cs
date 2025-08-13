namespace Domain.Classes.Entities
{
    public sealed class ProjectCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ProjectCategory(int id, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Project category name is required.", nameof(name));
            }

            Id = id;
            Name = name.Trim();
        }

    }
}
