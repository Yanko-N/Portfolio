using Application.Classes;
using Application.Contracts.DTOs;
using Application.Interfaces;
using Application.Interfaces.Services;
using Application.Mappings;

namespace Application.Services.JsonBased
{
    public class PersonalInfoJsonRepository : IPersonalInfoService
    {
        private readonly IManifestClient _manifestClient;
        public PersonalInfoJsonRepository(IManifestClient manifestClient)
        {
            _manifestClient = manifestClient;

        }

        public async Task<Result<Response.PersonalInfoDto>> GetPersonalInfoAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                IEnumerable<Persistence.Models.PersonalInfo> personalInfoList = await _manifestClient.GetAsync<Persistence.Models.PersonalInfo>(cancellationToken);

                if (personalInfoList == null || !personalInfoList.Any())
                {
                    return Result<Response.PersonalInfoDto>.Failure("No personal information found.");
                }

                var personalInfo = personalInfoList.FirstOrDefault();

                IEnumerable<Persistence.Models.PersonalInfoImages> personalInfoImagesList = await _manifestClient.GetAsync<Persistence.Models.PersonalInfoImages>(cancellationToken);

                if (personalInfoImagesList == null || !personalInfoImagesList.Any())
                {
                    return Result<Response.PersonalInfoDto>.Failure("No personal information images found.");
                }
                var personalInfoImages = personalInfoImagesList.FirstOrDefault(x=>x.PersonalInfoId == personalInfo.Id);

                if (personalInfoImages == null)
                {
                    return Result<Response.PersonalInfoDto>.Failure("No personal information images found for the given personal info ID.");
                }

                //get the profile image
                IEnumerable<Persistence.Models.Image> imagesList = await _manifestClient.GetAsync<Persistence.Models.Image>(cancellationToken);

                if (imagesList == null || !imagesList.Any())
                {
                    return Result<Response.PersonalInfoDto>.Failure("No images found.");
                }

                var profileImageModel = imagesList.FirstOrDefault(x => x.Id == personalInfoImages.ImageId);

                var image = profileImageModel != null
                    ? new Domain.Classes.Entities.Image(profileImageModel.Id, profileImageModel.Url, profileImageModel.Path, profileImageModel.AltText)
                    : null;

                var validatedPersonalInfo = new Domain.Classes.Entities.PersonalInfo(personalInfo.Id,
                    image,
                    personalInfo.Name,
                    personalInfo.Description,
                    personalInfo.WorkTitle,
                    personalInfo.EndPointCV,
                    new Domain.Classes.Values.Email(personalInfo.Email),
                    personalInfo.CellphoneNumber,
                    personalInfo.Address);

                var personalInfoDto = validatedPersonalInfo.ToDto();

                if(personalInfoDto == null)
                {
                    return Result<Response.PersonalInfoDto>.Failure("Failed to map personal information to DTO.");
                }

                return Result<Response.PersonalInfoDto>.Success(personalInfoDto);
            }
            catch(Exception ex)
            {
                return Result<Response.PersonalInfoDto>.Failure($"An Exception Ocurred");
            }
        }
    }
}
