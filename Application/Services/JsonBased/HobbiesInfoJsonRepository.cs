using Application.Classes;
using Application.Contracts.DTOs;
using Application.Interfaces;
using Application.Interfaces.Services;
using Application.Mappings;
using Domain.Classes.Entities;

namespace Application.Services.JsonBased
{
    public class HobbiesInfoJsonRepository : IHobbiesService
    {
        private readonly IManifestClient _manifestClient;
        public HobbiesInfoJsonRepository(IManifestClient manifestClient)
        {
            _manifestClient = manifestClient;
        }

        public async Task<Result<IEnumerable<Response.HobbyDto>>> GetHobbiesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                IEnumerable<Persistence.Models.Hobby> hobbiesList = await _manifestClient.GetAsync<Persistence.Models.Hobby>( cancellationToken);

                if (hobbiesList == null || !hobbiesList.Any())
                {
                    return Result<IEnumerable<Response.HobbyDto>>.Failure("No hobbies information found.");
                }

                IEnumerable<Persistence.Models.HobbyImages> hobbyImagesList = await _manifestClient.GetAsync<Persistence.Models.HobbyImages>( cancellationToken);

                IEnumerable<Persistence.Models.Image> imagesList = await _manifestClient.GetAsync<Persistence.Models.Image>(cancellationToken);

                List<Response.HobbyDto> toBeReturn = new List<Response.HobbyDto>();
                foreach (var hobbyPersistence in hobbiesList)
                {
                    var hobbyImage = hobbyImagesList.Where(x => x.HobbyId == hobbyPersistence.Id).FirstOrDefault();
                   
                    var image = imagesList?.Where(x => x.Id == hobbyImage?.ImageId).FirstOrDefault() ?? null;

                    Image validatedImage = null;

                    if(image != null)
                    {
                        try
                        {
                            validatedImage = new Image(image.Id,image.Url,image.Path,image.AltText);
                        }
                        catch
                        {
                            //Don't do anything, cuz the image is not needed
                        }
                    }

                    Hobby domainObject = null;
                    try
                    {
                        domainObject = new Hobby(hobbyPersistence.Id,hobbyPersistence.Name,hobbyPersistence.Description,validatedImage,hobbyPersistence.Icon);
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
                    return Result<IEnumerable<Response.HobbyDto>>.Failure("No valid hobby information found.");
                }

                return Result<IEnumerable<Response.HobbyDto>>.Success(toBeReturn);
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<Response.HobbyDto>>.Failure($"An Exception Ocurred");
            }
        }
    }
}
