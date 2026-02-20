using Application.Classes;
using Application.Contracts.DTOs;

namespace Application.Interfaces.Services
{
    public interface ISocialsService
    {
        Task<Result<IEnumerable<Response.SocialDto>>> GetSocialsAsync(CancellationToken cancellationtoken = default);
    }
}
