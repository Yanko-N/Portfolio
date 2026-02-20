using Application.Classes;
using Application.Contracts.DTOs;

namespace Application.Interfaces.Services
{
    public interface IEducationService
    {
        Task<Result<IEnumerable<Response.EducationDto>>> GetEducationsAsync(CancellationToken cancellationToken = default);
        Task<Result<Response.EducationDto>> GetEducationAsync(int id,CancellationToken cancellationToken = default);

    }
}
