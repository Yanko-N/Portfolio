using Application.Classes;
using Application.Contracts.DTOs;

namespace Application.Interfaces.Services
{
    public interface ISkillsService
    {
        Task<Result<IEnumerable<Response.SkillDto>>> GetSkillsAsync(CancellationToken cancellationToken = default);
    }
}
