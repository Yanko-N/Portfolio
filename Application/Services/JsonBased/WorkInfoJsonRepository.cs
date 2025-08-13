using Application.Classes;
using Application.Contracts.DTOs;
using Application.Interfaces;
using Application.Interfaces.Services;
using Application.Mappings;
using Domain.Classes.Entities;

namespace Application.Services.JsonBased
{
    public class WorkInfoJsonRepository : IWorkService
    {
        private readonly IManifestClient _manifestClient;

        public WorkInfoJsonRepository(IManifestClient manifestClient)
        {
            _manifestClient = manifestClient;
        }

        public async Task<Result<IEnumerable<Response.WorkDto>>> GetWorksAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                IEnumerable<Response.WorkDto> toBeReturn = new List<Response.WorkDto>();

                IEnumerable<Persistence.Models.Work> workList = await _manifestClient.GetAsync<Persistence.Models.Work>(cancellationToken);

                if (workList == null || !workList.Any())
                {
                    return Result<IEnumerable<Response.WorkDto>>.Failure("No work information found");
                }

                IEnumerable<Persistence.Models.Skill> skillsList = await _manifestClient.GetAsync<Persistence.Models.Skill>(cancellationToken);

                if (skillsList == null || !skillsList.Any())
                {
                    return Result<IEnumerable<Response.WorkDto>>.Failure("No skills information found.");
                }

                IEnumerable<Persistence.Models.WorkSkills> workSkillsList = await _manifestClient.GetAsync<Persistence.Models.WorkSkills>(cancellationToken);

                if (workSkillsList == null || !workSkillsList.Any())
                {
                    return Result<IEnumerable<Response.WorkDto>>.Failure("No work skills information found");
                }

                IEnumerable<Persistence.Models.Enterprise> enterprisesList = await _manifestClient.GetAsync<Persistence.Models.Enterprise>(cancellationToken);

                if (enterprisesList == null || !enterprisesList.Any())
                {
                    return Result<IEnumerable<Response.WorkDto>>.Failure("No work enterpries information found");
                }

                IEnumerable<Persistence.Models.SkillCategory> skillCategoryList = await _manifestClient.GetAsync<Persistence.Models.SkillCategory>(cancellationToken);
                IEnumerable<Persistence.Models.Image> imagesList = await _manifestClient.GetAsync<Persistence.Models.Image>(cancellationToken);

                foreach (var work in workList)
                {
                    Domain.Classes.Entities.Work workDomain = null;

                    Persistence.Models.Enterprise thisWorkEnterprise = enterprisesList.FirstOrDefault(x => x.Id == work.EnterpriseId);

                    if(thisWorkEnterprise == null)
                    {
                        //invalido
                        continue;
                    }

                    Persistence.Models.Image thisEnterpriseImage = imagesList.FirstOrDefault(x => x.Id == thisWorkEnterprise.ImageId);

                    Image validatedImage = null;

                    if (thisEnterpriseImage != null)
                    {
                        try
                        {
                            validatedImage = new Image(thisEnterpriseImage.Id, thisEnterpriseImage.Url, thisEnterpriseImage.Path, thisEnterpriseImage.AltText);
                        }
                        catch
                        {
                            //Don't do anything, cuz the image is not needed
                        }
                    }

                    Domain.Classes.Entities.Enterprise enterpriseInDomain = null;

                    try
                    {
                        enterpriseInDomain = new Domain.Classes.Entities.Enterprise(thisWorkEnterprise.Id, thisWorkEnterprise.Name,thisWorkEnterprise.Url, validatedImage);
                    }
                    catch
                    {
                        //Invalid Hobby, skip it
                        continue;
                    }

                    //Skills agora

                    IEnumerable<Persistence.Models.WorkSkills> workSkills = workSkillsList.Where(x => x.WorkId == work.Id);

                    IEnumerable<Domain.Classes.Entities.Skill> workSkillsInDomain = new List<Domain.Classes.Entities.Skill>();
                    foreach (var workSkill in workSkills)
                    {
                        var thisWorkSkill = skillsList.FirstOrDefault(x => x.Id == workSkill.SkillId);

                        if(thisWorkSkill == null)
                        {
                            continue;
                        }

                        var category = skillCategoryList.Where(x => x.Id == workSkill.SkillId).FirstOrDefault();

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
                            //Invalid Hobby, skip it
                            continue;
                        }

                        workSkillsInDomain.Append(skillInDomainObject);
                    }

                    Work workInDomain = null;
                    try
                    {
                        workInDomain = new Work(work.Id,enterpriseInDomain,new Domain.Classes.Values.DateRange(work.StartDate,work.EndDate),work.Title,work.Description,workSkillsInDomain);
                    }
                    catch
                    {
                        //Invalid Hobby, skip it
                        continue;
                    }

                    toBeReturn.Append(workInDomain.ToDto());

                }

                if (!toBeReturn.Any())
                {
                    return Result<IEnumerable<Response.WorkDto>>.Failure("No valid work information found.");
                }

                return Result<IEnumerable<Response.WorkDto>>.Success(toBeReturn);
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<Response.WorkDto>>.Failure($"An Exception Ocurred");
            }
        }
    }
}
