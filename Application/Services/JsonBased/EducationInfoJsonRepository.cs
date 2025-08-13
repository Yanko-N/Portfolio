using Application.Classes;
using Application.Contracts.DTOs;
using Application.Interfaces;
using Application.Interfaces.Services;
using Application.Mappings;

namespace Application.Services.JsonBased
{
    public class EducationInfoJsonRepository : IEducationService
    {
        private readonly IManifestClient _manifestClient;

        public EducationInfoJsonRepository(IManifestClient manifestClient)
        {
            _manifestClient = manifestClient;
        }

        public async Task<Result<IEnumerable<Response.EducationDto>>> GetEducationsAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                List<Response.EducationDto> toBeReturned = new List<Response.EducationDto> ();

                IEnumerable<Persistence.Models.Education> educationsList = await _manifestClient.GetAsync<Persistence.Models.Education>(cancellationToken);

                if (educationsList == null || !educationsList.Any())
                {
                    return Result<IEnumerable<Response.EducationDto>>.Failure("No Education information found");
                }

                IEnumerable<Persistence.Models.School> schoolsList = await _manifestClient.GetAsync<Persistence.Models.School>(cancellationToken);

                if (schoolsList == null || !schoolsList.Any())
                {
                    return Result<IEnumerable<Response.EducationDto>>.Failure("No schools information found");
                }

                IEnumerable<Persistence.Models.Course> cousesList = await _manifestClient.GetAsync<Persistence.Models.Course>(cancellationToken);

                if (cousesList == null || !cousesList.Any())
                {
                    return Result<IEnumerable<Response.EducationDto>>.Failure("No courses information found");
                }

                IEnumerable<Persistence.Models.Skill> skillsList = await _manifestClient.GetAsync<Persistence.Models.Skill>(cancellationToken);

                if (skillsList == null || !skillsList.Any())
                {
                    return Result<IEnumerable<Response.EducationDto>>.Failure("No skills information found.");
                }

                //not enforced
                IEnumerable<Persistence.Models.SkillCategory> skillCategoryList = await _manifestClient.GetAsync<Persistence.Models.SkillCategory>(cancellationToken);
                IEnumerable<Persistence.Models.EducationSkills> educationSkills = await _manifestClient.GetAsync<Persistence.Models.EducationSkills>(cancellationToken);

                foreach (var education in educationsList)
                {
                    var relatedSchool = schoolsList.FirstOrDefault(x => x.Id == education.SchoolId);
                    if(relatedSchool == null)
                    {
                        //ignorar
                        continue;
                    }

                    Domain.Classes.Entities.School domainSchool = null;
                    try
                    {
                        domainSchool = new Domain.Classes.Entities.School(relatedSchool.Id, relatedSchool.Name, relatedSchool.Location);

                    }
                    catch (Exception ex)
                    {
                        //ignorar
                        continue;
                    }

                    var relatedCourse = cousesList.FirstOrDefault(x => x.Id == education.CourseId);
                    if (relatedCourse == null)
                    {
                        //ignorar
                        continue;
                    }

                    Domain.Classes.Entities.Course domainCourse = null;
                    try
                    {
                        domainCourse = new Domain.Classes.Entities.Course(relatedCourse.Id, relatedCourse.Name, relatedCourse.Description, relatedCourse.Url);
                    }
                    catch (Exception ex)
                    {
                        //ignorar invalido
                        continue;
                    }

                    //Skills agora
                    IEnumerable<Persistence.Models.EducationSkills> thisEducationSkills = educationSkills.Where(x => x.EducationId == education.Id);

                    IEnumerable<Domain.Classes.Entities.Skill> educationSkillsInDomain = new List<Domain.Classes.Entities.Skill>(); //To be added to the skills
                    foreach (var educationSkill in thisEducationSkills)
                    {
                        var thisWorkSkill = skillsList.FirstOrDefault(x => x.Id == educationSkill.SkillId);

                        if (thisWorkSkill == null)
                        {
                            continue;
                        }

                        var category = skillCategoryList.Where(x => x.Id == educationSkill.SkillId).FirstOrDefault();

                        Domain.Classes.Entities.SkillCategory validatedSkillCategory = null;
                        if (category != null)
                        {
                            try
                            {
                                validatedSkillCategory = new Domain.Classes.Entities.SkillCategory(category.Id, category.Name);
                            }
                            catch
                            {
                                continue;
                            }
                        }

                        Domain.Classes.Entities.Skill skillInDomainObject = null;
                        try
                        {
                            skillInDomainObject = new Domain.Classes.Entities.Skill(thisWorkSkill.Id, thisWorkSkill.Icon, thisWorkSkill.Description, thisWorkSkill.Name, thisWorkSkill.Motivation, validatedSkillCategory);
                        }
                        catch
                        {
                            //Invalid education, skip it
                            continue;
                        }

                        educationSkillsInDomain.Append(skillInDomainObject);
                    }

                    Domain.Classes.Entities.Education domainEducation = null;
                    try
                    {
                        domainEducation = new Domain.Classes.Entities.Education(education.Id,domainSchool,domainCourse,new Domain.Classes.Values.DateRange(education.StartDate,education.EndDate), educationSkillsInDomain);
                    }
                    catch (Exception ex)
                    {
                        //ignorar invalido
                        continue;
                    }

                    toBeReturned.Add(domainEducation.ToDto());
                }

                if (!toBeReturned.Any())
                {
                    return Result<IEnumerable<Response.EducationDto>>.Failure("No valid education information found.");
                }

                return Result<IEnumerable<Response.EducationDto>>.Success(toBeReturned);
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<Response.EducationDto>>.Failure($"An Exception Ocurred");
            }
        }

        public async Task<Result<Response.EducationDto>> GetEducationAsync(int id ,CancellationToken cancellationToken = default)
        {
            try
            {
                Response.EducationDto toBeReturned = null;

                IEnumerable<Persistence.Models.Education> educationsList = await _manifestClient.GetAsync<Persistence.Models.Education>(cancellationToken);

                if (educationsList == null || !educationsList.Any())
                {
                    return Result<Response.EducationDto>.Failure("No Education information found");
                }

                IEnumerable<Persistence.Models.School> schoolsList = await _manifestClient.GetAsync<Persistence.Models.School>(cancellationToken);

                if (schoolsList == null || !schoolsList.Any())
                {
                    return Result<Response.EducationDto>.Failure("No schools information found");
                }

                IEnumerable<Persistence.Models.Course> cousesList = await _manifestClient.GetAsync<Persistence.Models.Course>(cancellationToken);

                if (cousesList == null || !cousesList.Any())
                {
                    return Result<Response.EducationDto>.Failure("No courses information found");
                }

                IEnumerable<Persistence.Models.Skill> skillsList = await _manifestClient.GetAsync<Persistence.Models.Skill>(cancellationToken);

                if (skillsList == null || !skillsList.Any())
                {
                    return Result<Response.EducationDto>.Failure("No skills information found.");
                }

                //not enforced
                IEnumerable<Persistence.Models.SkillCategory> skillCategoryList = await _manifestClient.GetAsync<Persistence.Models.SkillCategory>(cancellationToken);
                IEnumerable<Persistence.Models.EducationSkills> educationSkills = await _manifestClient.GetAsync<Persistence.Models.EducationSkills>(cancellationToken);

                foreach (var education in educationsList)
                {
                    if(education.Id != id)
                    {
                        continue;
                    }

                    var relatedSchool = schoolsList.FirstOrDefault(x => x.Id == education.SchoolId);
                    if (relatedSchool == null)
                    {
                        //ignorar
                        continue;
                    }

                    Domain.Classes.Entities.School domainSchool = null;
                    try
                    {
                        domainSchool = new Domain.Classes.Entities.School(relatedSchool.Id, relatedSchool.Name, relatedSchool.Location);

                    }
                    catch (Exception ex)
                    {
                        //ignorar
                        continue;
                    }

                    var relatedCourse = cousesList.FirstOrDefault(x => x.Id == education.CourseId);
                    if (relatedCourse == null)
                    {
                        //ignorar
                        continue;
                    }

                    Domain.Classes.Entities.Course domainCourse = null;
                    try
                    {
                        domainCourse = new Domain.Classes.Entities.Course(relatedCourse.Id, relatedCourse.Name, relatedCourse.Description, relatedCourse.Url);
                    }
                    catch (Exception ex)
                    {
                        //ignorar invalido
                        continue;
                    }

                    //Skills agora
                    IEnumerable<Persistence.Models.EducationSkills> thisEducationSkills = educationSkills.Where(x => x.EducationId == education.Id);

                    IEnumerable<Domain.Classes.Entities.Skill> educationSkillsInDomain = new List<Domain.Classes.Entities.Skill>(); //To be added to the skills
                    foreach (var educationSkill in thisEducationSkills)
                    {
                        var thisWorkSkill = skillsList.FirstOrDefault(x => x.Id == educationSkill.SkillId);

                        if (thisWorkSkill == null)
                        {
                            continue;
                        }

                        var category = skillCategoryList.Where(x => x.Id == educationSkill.SkillId).FirstOrDefault();

                        Domain.Classes.Entities.SkillCategory validatedSkillCategory = null;
                        if (category != null)
                        {
                            try
                            {
                                validatedSkillCategory = new Domain.Classes.Entities.SkillCategory(category.Id, category.Name);
                            }
                            catch
                            {
                                continue;
                            }
                        }

                        Domain.Classes.Entities.Skill skillInDomainObject = null;
                        try
                        {
                            skillInDomainObject = new Domain.Classes.Entities.Skill(thisWorkSkill.Id, thisWorkSkill.Icon, thisWorkSkill.Description, thisWorkSkill.Name, thisWorkSkill.Motivation, validatedSkillCategory);
                        }
                        catch
                        {
                            //Invalid education, skip it
                            continue;
                        }

                        educationSkillsInDomain.Append(skillInDomainObject);
                    }

                    Domain.Classes.Entities.Education domainEducation = null;
                    try
                    {
                        domainEducation = new Domain.Classes.Entities.Education(education.Id, domainSchool, domainCourse, new Domain.Classes.Values.DateRange(education.StartDate, education.EndDate), educationSkillsInDomain);
                    }
                    catch (Exception ex)
                    {
                        //ignorar invalido
                        continue;
                    }

                    toBeReturned = domainEducation.ToDto();
                }

                if(toBeReturned == null)
                {
                    return Result<Response.EducationDto>.Failure("No valid education information found with the given Id");
                }

                return Result<Response.EducationDto>.Success(toBeReturned);
            }
            catch (Exception ex)
            {
                return Result<Response.EducationDto>.Failure($"An Exception Ocurred");
            }
        }

    }
}
