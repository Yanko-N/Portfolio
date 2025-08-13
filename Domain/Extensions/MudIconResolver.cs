using MudBlazor;
namespace Domain.Extensions
{
    public static class MudIconResolver
    {
        public static string ResolveMudBlazorIcon(this string icon, IconVariant variant = IconVariant.Filled)
        {
            if (string.IsNullOrWhiteSpace(icon))
            {
                return Icons.Material.Filled.Link;
            }

            icon = icon.Trim().ToLowerInvariant();

            switch (icon)
            {
                case "github":
                    return Icons.Custom.Brands.GitHub;

                case "linkedin":
                    return Icons.Custom.Brands.LinkedIn;

                case "twitter":
                    return Icons.Custom.Brands.Twitter;

                case "website":
                case "link":
                    return Icons.Material.Filled.Link;

                case "email":
                case "mail":
                    return variant == IconVariant.Outlined
                        ? Icons.Material.Outlined.Email
                        : Icons.Material.Filled.Email;

                case "phone":
                    return variant == IconVariant.Outlined
                        ? Icons.Material.Outlined.Phone
                        : Icons.Material.Filled.Phone;

                case "location":
                    return Icons.Material.Filled.LocationOn;

                case "videogame":
                    return Icons.Material.Filled.VideogameAsset;

                case "skateboarding":
                    return Icons.Material.Filled.Skateboarding;

                case "hiking":
                    return Icons.Material.Filled.Hiking;

                default:
                    return Icons.Material.Filled.Link;
            }
        }
    }
    public enum IconVariant { Filled, Outlined }

}
