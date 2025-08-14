using Application.Classes;
using Application.Contracts.DTOs;
using Application.Interfaces;
using Application.Interfaces.Services;
using Application.Mappings;
using Domain.Classes.Entities;

namespace Application.Services.JsonBased
{
    public class ProjectsInfoJsonRepository : IProjectService
    {
        private readonly IManifestClient _manifestClient;

        public ProjectsInfoJsonRepository(IManifestClient manifestClient)
        {
            _manifestClient = manifestClient;
        }

        public async Task<Result<Response.ProjectDto>> GetProjectAsync(int id, CancellationToken cancellationToken = default)
        {
            try
            {

                Response.ProjectDto toBeReturned = null;

                IEnumerable<Persistence.Models.Project> persistenceProjectsList = await _manifestClient.GetAsync<Persistence.Models.Project>(cancellationToken);

                if (persistenceProjectsList == null || !persistenceProjectsList.Any())
                {
                    return Result<Response.ProjectDto>.Failure("No projects found");
                }

                IEnumerable<Persistence.Models.Skill> persistenceSkillsList = await _manifestClient.GetAsync<Persistence.Models.Skill>(cancellationToken);

                if (persistenceSkillsList == null || !persistenceSkillsList.Any())
                {
                    return Result<Response.ProjectDto>.Failure("No skills information found.");
                }

                IEnumerable<Persistence.Models.ProjectCategory> persistenceProjectCategoriesList = await _manifestClient.GetAsync<Persistence.Models.ProjectCategory>(cancellationToken);

                if (persistenceProjectCategoriesList == null || !persistenceProjectCategoriesList.Any())
                {
                    return Result<Response.ProjectDto>.Failure("No project categories information found.");
                }

                IEnumerable<Persistence.Models.ProjectImages> persistenceProjectImagesList = await _manifestClient.GetAsync<Persistence.Models.ProjectImages>(cancellationToken);

                if (persistenceProjectImagesList == null || !persistenceProjectImagesList.Any())
                {
                    return Result<Response.ProjectDto>.Failure("No project images information found.");
                }

                //Can be null not enforced
                IEnumerable<Persistence.Models.ProjectSkills> persistenceProjectSkills = await _manifestClient.GetAsync<Persistence.Models.ProjectSkills>(cancellationToken);
                IEnumerable<Persistence.Models.SkillCategory> skillCategoryList = await _manifestClient.GetAsync<Persistence.Models.SkillCategory>(cancellationToken);
                IEnumerable<Persistence.Models.Image> imagesList = await _manifestClient.GetAsync<Persistence.Models.Image>(cancellationToken);

                foreach (var persistenceProject in persistenceProjectsList)
                {
                    if(persistenceProject.Id != id)
                    {
                        //ignore
                        continue;
                    }

                    Project projectDomain = null;

                    IEnumerable<Image> imageList = new List<Image>();

                    var currentConnectedImages = persistenceProjectImagesList.Where(x => x.ProjectId == persistenceProject.Id);

                    foreach (var persistenceImage in imageList)
                    {
                        Persistence.Models.Image thisProjectImage = imagesList.FirstOrDefault(x => x.Id == persistenceProject.Id);

                        Image validatedImage = null;

                        if (thisProjectImage != null)
                        {
                            try
                            {
                                validatedImage = new Image(thisProjectImage.Id, thisProjectImage.Url, thisProjectImage.Path, thisProjectImage.AltText);
                            }
                            catch
                            {
                                //Don't do anything, cuz the image is not needed
                            }
                        }

                        imageList.Append(validatedImage);
                    }

                    var category = persistenceProjectCategoriesList.FirstOrDefault(x => x.Id == persistenceProject.ProjectCategoryId);

                    if (category == null)
                    {
                        //ignore this project
                        continue;
                    }

                    ProjectCategory projectCategoryDomain = null;

                    try
                    {
                        projectCategoryDomain = new ProjectCategory(category.Id, category.Name);
                    }
                    catch (Exception ex)
                    {
                        //ignore
                        continue;
                    }

                    //Skills agora

                    IEnumerable<Persistence.Models.ProjectSkills> projectSkills = persistenceProjectSkills.Where(x => x.ProjectId == persistenceProject.Id);

                    IEnumerable<Skill> projectSkillsInDomain = new List<Skill>();
                    foreach (var projectSkill in projectSkills)
                    {
                        var thisProjectSkill = persistenceSkillsList.FirstOrDefault(x => x.Id == projectSkill.SkillId);

                        if (thisProjectSkill == null)
                        {
                            continue;
                        }

                        var skillCategory = skillCategoryList.Where(x => x.Id == projectSkill.SkillId).FirstOrDefault();

                        SkillCategory validatedSkillCategory = null;
                        if (skillCategory != null)
                        {
                            try
                            {
                                validatedSkillCategory = new SkillCategory(skillCategory.Id, skillCategory.Name);
                            }
                            catch
                            {
                                continue;
                            }
                        }

                        Skill skillInDomainObject = null;
                        try
                        {
                            skillInDomainObject = new Skill(thisProjectSkill.Id, thisProjectSkill.Icon, thisProjectSkill.Description, thisProjectSkill.Name, thisProjectSkill.Motivation, validatedSkillCategory);
                        }
                        catch
                        {
                            //Invalid skill, skip it
                            continue;
                        }

                        projectSkillsInDomain.Append(skillInDomainObject);
                    }

                    try
                    {
                        projectDomain = new Project(persistenceProject.Id, persistenceProject.Name, persistenceProject.Description, persistenceProject.DemoUrl, persistenceProject.SourceCodeUrl,
                            projectCategoryDomain, projectSkillsInDomain, imageList);
                    }
                    catch (Exception ex)
                    {
                        //ignore
                        continue;
                    }

                    toBeReturned = projectDomain.ToDto();
                    break;
                }

                if (toBeReturned == null)
                {
                    return Result<Response.ProjectDto>.Failure("No projects information found");
                }

                return Result<Response.ProjectDto>.Success(toBeReturned);

            }
            catch (Exception ex)
            {
                return Result<Response.ProjectDto>.Failure("An Exception Ocurred");
            }
        }

        public async Task<Result<IEnumerable<Response.ProjectDto>>> GetProjectsAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                List<Response.ProjectDto> toBeReturned = new List<Response.ProjectDto>();

                IEnumerable<Persistence.Models.Project> persistenceProjectsList = await _manifestClient.GetAsync<Persistence.Models.Project>(cancellationToken);

                if (persistenceProjectsList == null || !persistenceProjectsList.Any())
                {
                    return Result<IEnumerable<Response.ProjectDto>>.Failure("No projects found");
                }

                IEnumerable<Persistence.Models.Skill> persistenceSkillsList = await _manifestClient.GetAsync<Persistence.Models.Skill>(cancellationToken);

                if (persistenceSkillsList == null || !persistenceSkillsList.Any())
                {
                    return Result<IEnumerable<Response.ProjectDto>>.Failure("No skills information found.");
                }

                IEnumerable<Persistence.Models.ProjectCategory> persistenceProjectCategoriesList = await _manifestClient.GetAsync<Persistence.Models.ProjectCategory>(cancellationToken);

                if (persistenceProjectCategoriesList == null || !persistenceProjectCategoriesList.Any())
                {
                    return Result<IEnumerable<Response.ProjectDto>>.Failure("No project categories information found.");
                }

                IEnumerable<Persistence.Models.ProjectImages> persistenceProjectImagesList = await _manifestClient.GetAsync<Persistence.Models.ProjectImages>(cancellationToken);

                if (persistenceProjectImagesList == null || !persistenceProjectImagesList.Any())
                {
                    return Result<IEnumerable<Response.ProjectDto>>.Failure("No project images information found.");
                }

                //Can be null not enforced
                IEnumerable<Persistence.Models.ProjectSkills> persistenceProjectSkills = await _manifestClient.GetAsync<Persistence.Models.ProjectSkills>(cancellationToken);
                IEnumerable<Persistence.Models.SkillCategory> skillCategoryList = await _manifestClient.GetAsync<Persistence.Models.SkillCategory>(cancellationToken);
                IEnumerable<Persistence.Models.Image> imagesList = await _manifestClient.GetAsync<Persistence.Models.Image>(cancellationToken);

                foreach(var persistenceProject in persistenceProjectsList)
                {
                    Project projectDomain = null;

                    IEnumerable<Image> imageList = new List<Image>();

                    var currentConnectedImages = persistenceProjectImagesList.Where(x => x.ProjectId == persistenceProject.Id);

                    foreach (var persistenceImage in imageList)
                    {
                        Persistence.Models.Image thisProjectImage = imagesList.FirstOrDefault(x => x.Id == persistenceProject.Id);

                        Image validatedImage = null;

                        if (thisProjectImage != null)
                        {
                            try
                            {
                                validatedImage = new Image(thisProjectImage.Id, thisProjectImage.Url, thisProjectImage.Path, thisProjectImage.AltText);
                            }
                            catch
                            {
                                //Don't do anything, cuz the image is not needed
                            }
                        }

                        imageList.Append(validatedImage);
                    }

                    var category = persistenceProjectCategoriesList.FirstOrDefault(x => x.Id == persistenceProject.ProjectCategoryId);

                    if (category == null)
                    {
                        //ignore this project
                        continue;
                    }

                    ProjectCategory projectCategoryDomain = null;

                    try
                    {
                        projectCategoryDomain = new ProjectCategory(category.Id, category.Name);
                    } 
                    catch (Exception ex)
                    {
                        //ignore
                        continue;
                    }

                    //Skills agora

                    IEnumerable<Persistence.Models.ProjectSkills> projectSkills = persistenceProjectSkills.Where(x => x.ProjectId == persistenceProject.Id);

                    IEnumerable<Skill> projectSkillsInDomain = new List<Skill>();
                    foreach (var projectSkill in projectSkills)
                    {
                        var thisProjectSkill = persistenceSkillsList.FirstOrDefault(x => x.Id == projectSkill.SkillId);

                        if (thisProjectSkill == null)
                        {
                            continue;
                        }

                        var skillCategory = skillCategoryList.Where(x => x.Id == projectSkill.SkillId).FirstOrDefault();

                        SkillCategory validatedSkillCategory = null;
                        if (skillCategory != null)
                        {
                            try
                            {
                                validatedSkillCategory = new SkillCategory(skillCategory.Id, skillCategory.Name);
                            }
                            catch
                            {
                                continue;
                            }
                        }

                        Skill skillInDomainObject = null;
                        try
                        {
                            skillInDomainObject = new Skill(thisProjectSkill.Id, thisProjectSkill.Icon, thisProjectSkill.Description, thisProjectSkill.Name, thisProjectSkill.Motivation, validatedSkillCategory);
                        }
                        catch
                        {
                            //Invalid skill, skip it
                            continue;
                        }

                        projectSkillsInDomain.Append(skillInDomainObject);
                    }

                    try
                    {
                        projectDomain = new Project(persistenceProject.Id, persistenceProject.Name, persistenceProject.Description, persistenceProject.DemoUrl, persistenceProject.SourceCodeUrl,
                            projectCategoryDomain, projectSkillsInDomain, imageList);
                    } 
                    catch (Exception ex)
                    {
                        //ignore
                        continue;
                    }

                    toBeReturned.Add(projectDomain.ToDto());
                }

                if (!toBeReturned.Any())
                {
                    return Result<IEnumerable<Response.ProjectDto>>.Failure("No projects information found");
                }

                return Result<IEnumerable<Response.ProjectDto>>.Success(toBeReturned);

            }
            catch (Exception ex)
            {
                return Result<IEnumerable<Response.ProjectDto>>.Failure("An Exception Ocurred");
            }
        }
    }
}
