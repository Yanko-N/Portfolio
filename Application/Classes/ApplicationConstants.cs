using Application.Enums;

namespace Application.Classes
{
    public static class ApplicationConstants
    {
        public static class Routes
        {
            public const string Index = "/";
            public const string SkillsPage = "/Skills";
            public const string HobbiesPage = "/Hobbies";
            public const string WorkPage = "/Works";
            public const string EducationPage = "/Educations";
            public const string ProjectsPage = "/Projects";

            public const string WorkDetail = "/Works/{id:int}";
            public const string EducationDetail = "/Educations/{id:int}";
            public const string ProjectsDetail = "/Projects/{id:int}";
            

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
