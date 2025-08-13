namespace Application.Mappings;

using Domain.Classes.Entities;
using static Application.Contracts.DTOs.Response;

public static class DtoMappers
{
    public static PersonalInfoDto ToDto(this PersonalInfo x) =>
        new PersonalInfoDto(
            x.Id,x.ProfileImage.ToDto(), x.Name, x.Description, x.WorkTitle, x.EndPointCV,
            x.Email.ToString(), x.CellphoneNumber, x.Address
        );

    public static LanguageDto ToDto(this Language x) =>
        new LanguageDto(
            x.Id, x.Name, x.Icon, x.SkillLevel
        );

    public static SkillDto ToDto(this Skill x) =>
        new SkillDto(
            x.Id, x.Icon, x.Description, x.Name, x.Motivation, x.Category.Name
        );
    public static SocialDto ToDto(this Social x) =>
        new SocialDto(x.Id,x.Name,x.Url.Value ?? string.Empty, x.Icon);

    public static ImageDto ToDto(this Image x) =>
        new ImageDto(
            x.Id, x.Url?.Value, x.Path, x.AltText
        );

    public static ProjectDto ToDto(this Project x) =>
        new ProjectDto(x.Id,x.Name,x.Description,x.DemoUrl?.Value,x.SourceCodeUrl?.Value,
            x.Category.Name, 
            x.Skills.Select(x=>x.ToDto()).ToList(),x.Images.Select(x=>x.ToDto()).ToList()
        );

    public static SchoolDto ToDto(this School x) => 
        new SchoolDto(x.Id, x.Name, x.Location);

    public static CourseDto ToDto(this Course x) =>
        new CourseDto(x.Id, x.Name, x.Description, x.Url.AbsoluteUri);

    public static EducationDto ToDto(this Education x) =>
        new EducationDto(x.Id, x.School.ToDto(), x.Course.ToDto(),
            x.Period.Start, x.Period.End,x.Skills.Select(x=>x.ToDto()).ToList()
        );

    public static EnterpriseDto ToDto(this Enterprise x) => 
        new EnterpriseDto( x.Id,x.Name,x.Url?.AbsolutePath,
            x.Image?.ToDto()
        );

    public static WorkDto ToDto(this Work x) =>
        new WorkDto(x.Id,x.Enterprise.ToDto(),x.Period.Start,x.Period.End,x.Title,x.Description,
            x.Skills.Select(x=>x.ToDto()).ToList()
        );
    public static HobbyDto ToDto(this Hobby x) =>
        new HobbyDto(x.Id, x.Name,x.Description, x.Image?.ToDto(),x.Icon);
}
