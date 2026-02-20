using Application.Enums;

namespace Application.Classes
{
    public static class ApplicationConstants
    {
        public static class Routes
        {
            public const string Index = "/";
            public const string SkillsPage = "/Portfolio/Skills";
            public const string HobbiesPage = "/Portfolio/Hobbies";
            public const string WorkPage = "/Portfolio/Works";
            public const string EducationPage = "/Portfolio/Educations";
            public const string ProjectsPage = "/Portfolio/Projects";

            public const string WorkDetail = "/Portfolio/Works/{id:int}";
            public const string EducationDetail = "/Portfolio/Educations/{id:int}";
            public const string ProjectsDetail = "/Portfolio/Projects/{id:int}";
            

            public static string GetDetailRoute(DetailsPageType type, int id)
            {
                string GetDetailRoute(string prefixRoute, int id)
                {
                    return $"{prefixRoute}/{id}";
                }

                string prefixRoute = type switch
                {
                    DetailsPageType.Work => WorkPage,
                    DetailsPageType.Education => EducationPage,
                    DetailsPageType.Project => ProjectsPage,
                    _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
                };
                return GetDetailRoute(prefixRoute,id);
            }
           

        }
    }
}
