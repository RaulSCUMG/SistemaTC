using SistemaTC.Data.Entities;

namespace SistemaTC.Services.Interfaces;
public interface IRoleService
{
    Task<List<Role>> GetRolesAsync();
}
