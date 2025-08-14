using Application.Classes;
using Application.Contracts.DTOs;

namespace Application.Interfaces.Services
{
    public interface IProjectService
    {
        Task<Result<IEnumerable<Response.ProjectDto>>> GetProjectsAsync(CancellationToken cancellationToken = default);
        Task<Result<Response.ProjectDto>> GetProjectAsync(int id, CancellationToken cancellationToken = default);
    }
}
