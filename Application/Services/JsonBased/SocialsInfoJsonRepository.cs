using Application.Classes;
using Application.Contracts.DTOs;
using Application.Interfaces;
using Application.Interfaces.Services;
using Application.Mappings;

namespace Application.Services.JsonBased
{
    public class SocialsInfoJsonRepository : ISocialsService
    {
        private readonly IManifestClient _manifestClient;

        public SocialsInfoJsonRepository(IManifestClient manifestClient)
        {
            _manifestClient = manifestClient;
        }
        public async Task<Result<IEnumerable<Response.SocialDto>>> GetSocialsAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                IEnumerable<Persistence.Models.Social> socialsList = await _manifestClient.GetAsync<Persistence.Models.Social>( cancellationToken);

                if (socialsList == null || !socialsList.Any())
                {
                    return Result<IEnumerable<Response.SocialDto>>.Failure("No social information found.");
                }
                
                var validatedSocials = socialsList.Select(x =>
                {
                    try 
                    { 
                        return new Domain.Classes.Entities.Social(  x.Id, x.Name, x.Url,x.Icon).ToDto();
                    }
                    catch (Exception ex)
                    {
                        return null;
                    }
                }).Where(x=> x != null);

                if (!validatedSocials.Any())
                {
                    return Result<IEnumerable<Response.SocialDto>>.Failure("No valid social information found.");
                }

                return Result<IEnumerable<Response.SocialDto>>.Success(validatedSocials);
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<Response.SocialDto>>.Failure($"An Exception Ocurred");
            }
        }
    }
}
