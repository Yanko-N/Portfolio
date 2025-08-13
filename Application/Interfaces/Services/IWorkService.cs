using Application.Classes;
using Application.Contracts.DTOs;

namespace Application.Interfaces.Services
{
    public interface IWorkService
    {
        Task<Result<IEnumerable<Response.WorkDto>>> GetWorksAsync(CancellationToken cancellationToken = default);
    }
}
