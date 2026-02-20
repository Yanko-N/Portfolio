using Application.Classes;
using Application.Contracts.DTOs;
using Application.Interfaces;
using Application.Interfaces.Services;
using Application.Mappings;
using Domain.Classes.Entities;

namespace Application.Services.JsonBased
{
    public class SkillsInfoJsonRepository : ISkillsService
    {

        private readonly IManifestClient _manifestClient;

        public SkillsInfoJsonRepository(IManifestClient manifestClient)
        {
            _manifestClient = manifestClient;
        }

        public async Task<Result<IEnumerable<Response.SkillDto>>> GetSkillsAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                IEnumerable<Persistence.Models.Skill> skillsList = await _manifestClient.GetAsync<Persistence.Models.Skill>(cancellationToken);

                if (skillsList == null || !skillsList.Any())
                {
                    return Result<IEnumerable<Response.SkillDto>>.Failure("No skills information found.");
                }

                IEnumerable<Persistence.Models.SkillCategory> skillCategoryList = await _manifestClient.GetAsync<Persistence.Models.SkillCategory>(cancellationToken);

                List<Response.SkillDto> toBeReturn = new List<Response.SkillDto>();
                foreach (var skill in skillsList)
                {
                    var category = skillCategoryList.Where(x => x.Id == skill.CategoryId).FirstOrDefault();

                  
                   SkillCategory validatedSkillCategory = null;
                    if (category != null)
                    {
                        try
                        {
                            validatedSkillCategory = new SkillCategory(category.Id,category.Name);
                        }
                        catch
                        {
                            continue;
                        }
                    }

                    Skill domainObject = null;
                    try
                    {
                        domainObject = new Skill(skill.Id,skill.Icon,skill.Description,skill.Name,skill.Motivation,validatedSkillCategory);
                    }
                    catch
                    {
                        //Invalid Hobby, skip it
                        continue;
                    }

                    toBeReturn.Add(domainObject.ToDto());


                }

                if (!toBeReturn.Any())
                {
                    return Result<IEnumerable<Response.SkillDto>>.Failure("No valid skills information found.");
                }

                return Result<IEnumerable<Response.SkillDto>>.Success(toBeReturn);
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<Response.SkillDto>>.Failure($"An Exception Ocurred");
            }
        }
    }
}
