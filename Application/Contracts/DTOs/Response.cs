namespace Application.Contracts.DTOs
{
    public static class Response
    {
        public sealed record PersonalInfoDto(
            int Id,
            ImageDto ProfileImage,
            string Name,
            string Description,
            string WorkTitle,
            string EndPointCV,
            string Email,
            string CellphoneNumber,
            string Address
        );

        public sealed record SocialDto(
            int Id,
            string Name,
            string Url,
            string Icon
        );

        public sealed record LanguageDto(
            int Id,
            string Name,
            string Icon,
            string SkillLevel
        );

        public sealed record SkillDto(
            int Id,
            string Icon,
            string Description,
            string Name,
            string? Motivation,
            string Category
        );

        public sealed record ImageDto(
            int Id,
            string? Url,
            string Path,
            string AltText
        );

        public sealed record ProjectDto(
            int Id,
            string Name,
            string Description,
            string? DemoUrl,
            string? SourceCodeUrl,
            string Category,
            IReadOnlyList<SkillDto> Skills,
            IReadOnlyList<ImageDto> Images
        );

        public sealed record SchoolDto(
            int Id,
            string Name,
            string Location
        );

        public sealed record CourseDto(
            int Id,
            string Name,
            string Description,
            string Url
        );

        public sealed record EducationDto(
            int Id,
            SchoolDto School,
            CourseDto Course,
            DateOnly StartDate,
            DateOnly? EndDate,
            IReadOnlyList<SkillDto> Skills
        );

        public sealed record EnterpriseDto(
            int Id,
            string Name,
            string? Url,
            ImageDto? Image
        );

        public sealed record WorkDto(
            int Id,
            EnterpriseDto Enterprise,
            DateOnly StartDate,
            DateOnly? EndDate,
            string Title,
            string Description,
            IReadOnlyList<SkillDto> Skills
        );

        public sealed record HobbyDto(
            int Id,
            string Name,
            string Description,
            ImageDto? Image,
            string Icon
        );

    }
    
}
