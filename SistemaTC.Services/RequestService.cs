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
        var validationErrors = ValidateRequest(request).ToList();

        if (validationErrors.Count is not 0)
        {
            return (null, validationErrors);
        }

        var user = await dbContext.Users.FirstAsync(x => x.UserId == request.RequestedByUserId);

        request.CreatedBy = user.UserName;
        await dbContext.Requests.AddAsync(request);
        await dbContext.SaveChangesAsync();

        return (request, []);
    }

    private IEnumerable<string> ValidateRequest(Request request, bool newRequest = true)
    {
        if(newRequest)
        {
            if (!dbContext.Users.Any(x => x.UserId == request.RequestedByUserId))
            {
                yield return "User doesn't exist";
            }
        }
        else
        {
            if (!dbContext.Users.Any(x => x.UserId == request.AssignedToUserId))
            {
                yield return "User doesn't exist";
            }
        }
    }
}
