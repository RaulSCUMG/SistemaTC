using Microsoft.EntityFrameworkCore;
using SistemaTC.Data;
using SistemaTC.Data.Entities;
using SistemaTC.Services.Interfaces;

namespace SistemaTC.Services;
public class RequestService(ITCContext dbContext) : IRequestService
{
    public async Task<List<Request>> GetRequestsAsync()
    {
        return await dbContext.Requests.ToListAsync();
    }

    public async Task<Request?> GetRequestAsync(Guid requestId)
    {
        return await dbContext.Requests.FirstOrDefaultAsync(x => x.RequestId == requestId);
    }

    public async Task<(Request? request, List<string> validationErrors)> AddAsync(Request request)
    {
        var validationErrors = await ValidateRequest(request).ToListAsync();

        if (validationErrors.Count is not 0)
        {
            return (null, validationErrors);
        }

        await dbContext.Requests.AddAsync(request);
        await dbContext.SaveChangesAsync();

        return (request, []);
    }

    public async Task<Request?> ProcessRequest(Request request)
    {
        var entity = await dbContext.Requests.FirstAsync(x => x.RequestId == request.RequestId);

        entity.AssignedToUserId = request.AssignedToUserId;
        entity.Approved = request.Approved;
        entity.InternalNote = request.InternalNote;
        entity.UpdatedBy = request.UpdatedBy;
        entity.Updated = request.Updated;

        await dbContext.SaveChangesAsync();

        return entity;
    }

    public async IAsyncEnumerable<string> ValidateRequest(Request request, bool newRequest = true)
    {
        if(newRequest)
        {
            if (!await dbContext.Users.AnyAsync(x => x.UserId == request.RequestedByUserId))
            {
                yield return "User doesn't exist";
            }
        }
        else
        {
            if(!await dbContext.Requests.AnyAsync(x => x.RequestId == request.RequestId))
            {
                yield return "Request doesn't exist";
            }

            if (!await dbContext.Users.AnyAsync(x => x.UserId == request.AssignedToUserId))
            {
                yield return "User doesn't exist";
            }
        }
    }
}
