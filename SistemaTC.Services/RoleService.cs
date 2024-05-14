using Microsoft.EntityFrameworkCore;
using SistemaTC.Data;
using SistemaTC.Data.Entities;
using SistemaTC.Services.Interfaces;

namespace SistemaTC.Services;
public class RoleService(ITCContext dbContext): IRoleService
{
    public async Task<List<Role>> GetRolesAsync()
    {
        return await dbContext.Roles.ToListAsync();
    }
}
