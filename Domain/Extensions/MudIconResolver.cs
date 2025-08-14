using MudBlazor;
namespace Domain.Extensions
{
    public static class MudIconResolver
    {
        public static string ResolveMudBlazorIcon(this string icon)
        {
            if (string.IsNullOrWhiteSpace(icon))
            {
                return Icons.Material.Filled.Link;
            }

            icon = icon.Trim().ToLowerInvariant();

            switch (icon)
            {
                case "unity":
                    return "<svg xmlns=\"http://www.w3.org/2000/svg\" width=\"16\" height=\"16\" fill=\"currentColor\" class=\"bi bi-unity\" viewBox=\"0 0 16 16\"> <path d=\"M15 11.2V3.733L8.61 0v2.867l2.503 1.466c.099.067.099.2 0 .234L8.148 6.3c-.099.067-.197.033-.263 0L4.92 4.567c-.099-.034-.099-.2 0-.234l2.504-1.466V0L1 3.733V11.2v-.033.033l2.438-1.433V6.833c0-.1.131-.166.197-.133L6.6 8.433c.099.067.132.134.132.234v3.466c0 .1-.132.167-.198.134L4.031 10.8l-2.438 1.433L7.983 16l6.391-3.733-2.438-1.434L9.434 12.3c-.099.067-.198 0-.198-.133V8.7c0-.1.066-.2.132-.233l2.965-1.734c.099-.066.197 0 .197.134V9.8z\"/> </svg>";
                
                case "github":
                    return Icons.Custom.Brands.GitHub;
                
                case "bootstrap":
                    return "<svg xmlns=\"http://www.w3.org/2000/svg\" width=\"16\" height=\"16\" fill=\"currentColor\" class=\"bi bi-bootstrap\" viewBox=\"0 0 16 16\"> <path d=\"M5.062 12h3.475c1.804 0 2.888-.908 2.888-2.396 0-1.102-.761-1.916-1.904-2.034v-.1c.832-.14 1.482-.93 1.482-1.816 0-1.3-.955-2.11-2.542-2.11H5.062zm1.313-4.875V4.658h1.78c.973 0 1.542.457 1.542 1.237 0 .802-.604 1.23-1.764 1.23zm0 3.762V8.162h1.822c1.236 0 1.887.463 1.887 1.348 0 .896-.627 1.377-1.811 1.377z\"/> <path d=\"M0 4a4 4 0 0 1 4-4h8a4 4 0 0 1 4 4v8a4 4 0 0 1-4 4H4a4 4 0 0 1-4-4zm4-3a3 3 0 0 0-3 3v8a3 3 0 0 0 3 3h8a3 3 0 0 0 3-3V4a3 3 0 0 0-3-3z\"/> </svg>";
                
                case "linkedin":
                    return Icons.Custom.Brands.LinkedIn;

                case "twitter":
                    return Icons.Custom.Brands.Twitter;

                case "website":
                case "link":
                    return Icons.Material.Filled.Link;

                case "email":
                case "mail":
                    return Icons.Material.Filled.Email;

                case "phone":
                    return Icons.Material.Filled.Phone;

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

}
