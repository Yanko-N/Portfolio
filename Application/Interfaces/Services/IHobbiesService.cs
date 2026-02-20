using Application.Classes;
using Application.Contracts.DTOs;

namespace Application.Interfaces.Services
{
    public interface IHobbiesService
    {
        Task<Result<IEnumerable<Response.HobbyDto>>> GetHobbiesAsync(CancellationToken cancellationToken = default);
    }
}
