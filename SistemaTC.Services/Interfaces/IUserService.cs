using SistemaTC.Data.Entities;

namespace SistemaTC.Services.Interfaces;
public interface IUserService
{
    Task<List<User>> GetUsersAsync();
}
