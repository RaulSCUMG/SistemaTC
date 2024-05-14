using SistemaTC.Data.Entities;

namespace SistemaTC.Services.Interfaces;
public interface IUserService
{
    Task<List<User>> GetUsersAsync();
    Task<User?> GetUserAsync(Guid userId);
    Task<User?> GetUserAsync(string userName, string password);
    Task<(User? user, List<string> validationErrors)> AddAsync(User user);
    Task<(User? user, List<string> validationErrors)> UpdateAsync(User user);
}
