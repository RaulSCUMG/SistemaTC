using SistemaTC.Data.Entities;

namespace SistemaTC.Services.Interfaces;
public interface IRequestService
{
    Task<List<Request>> GetRequestsAsync();
    Task<Request?> GetRequestAsync(Guid requestId);
    Task<(Request? request, List<string> validationErrors)> AddAsync(Request request);
}
