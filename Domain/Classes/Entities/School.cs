namespace Domain.Classes.Entities
{
    public sealed class School
    {
        public int Id { get; }
        public string Name { get; }
        public string Location { get; }

        public School(int id, string name, string location)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("School name is required.");
            }

            Id = id;
            Name = name.Trim();
            Location = location ?? string.Empty;
        }
    }
}
