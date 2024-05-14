using Microsoft.EntityFrameworkCore;
using SistemaTC.Data;
using SistemaTC.Data.Entities;
using SistemaTC.Services.Interfaces;

namespace SistemaTC.Services;

public class UserService(ITCContext dbContext) : IUserService
{
    public async Task<List<User>> GetUsersAsync()
    {
        return await dbContext.Users.ToListAsync();
    }
}
