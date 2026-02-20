using Domain.Extensions;

namespace Domain.Classes.Entities
{
    public sealed class Hobby
    {
        public int Id { get; }
        public string Name { get; }
        public string Description { get; }
        public Image? Image { get; }
        public string Icon { get; }

        public Hobby(int id, string name, string description, Image? image, string icon)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Hobby name is required.");
            }

            Id = id;
            Name = name.Trim();
            Description = description ?? string.Empty;
            Image = image;
            Icon = icon.ResolveMudBlazorIcon() ?? string.Empty;
        }
    }
}
