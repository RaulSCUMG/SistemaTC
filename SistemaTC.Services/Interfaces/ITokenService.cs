using SistemaTC.Data.Entities;

namespace SistemaTC.Services.Interfaces;
public interface ITokenService
{
    string CreateToken(User user);
}
