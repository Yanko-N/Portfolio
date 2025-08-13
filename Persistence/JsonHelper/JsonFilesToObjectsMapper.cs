namespace Persistence.JsonHelper
{
    public static class JsonFilesToObjectsMapper
    {
        private static readonly Dictionary<Type, string> _typeToKeyMap = new()
        {
            { typeof(Persistence.Models.Course), "courses" },
            { typeof(Persistence.Models.Education), "educations" },
            { typeof(Persistence.Models.EducationSkills), "educationSkills" },
            { typeof(Persistence.Models.Enterprise), "enterprises" },
            { typeof(Persistence.Models.Hobby), "hobbies" },
            { typeof(Persistence.Models.HobbyImages), "hobbiesImages" },
            { typeof(Persistence.Models.Image), "images" },
            { typeof(Persistence.Models.Language), "languages" },
            { typeof(Persistence.Models.PersonalInfo), "personalInfo" },
            { typeof(Persistence.Models.ProjectCategory), "projectsCategories" },
            { typeof(Persistence.Models.ProjectImages), "projectImages" },
            { typeof(Persistence.Models.Project), "projects" },
            { typeof(Persistence.Models.ProjectSkills), "projectSkills" },
            { typeof(Persistence.Models.School), "schools" },
            { typeof(Persistence.Models.SkillCategory), "skillCategories" },
            { typeof(Persistence.Models.Skill), "skills" },
            { typeof(Persistence.Models.Social), "socialsLinks" },
            { typeof(Persistence.Models.Work), "works" },
            { typeof(Persistence.Models.WorkSkills), "workSkills" },
            { typeof(Persistence.Models.PersonalInfoImages), "personalInfoImages" }
        };
        public static string GetKey<T>() 
        {
            if (_typeToKeyMap.TryGetValue(typeof(T), out var key))
            {
                return key;
            }

            throw new KeyNotFoundException($"No mapping found for type {typeof(T).Name}");
        }
    }
}
