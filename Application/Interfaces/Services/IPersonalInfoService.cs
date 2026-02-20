using Application.Classes;
using static Application.Contracts.DTOs.Response;

namespace Application.Interfaces.Services
{
    public interface IPersonalInfoService
    {
        Task<Result<PersonalInfoDto>> GetPersonalInfoAsync(CancellationToken cancellationToken = default);
    }
}
