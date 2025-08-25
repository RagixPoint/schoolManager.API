using SchoolManager.Domain.Entities;

namespace SchoolManager.Application.Interfaces.Auth;

public interface IJwtService
{
    string GenerateToken(User user);
    bool ValidateToken(string token);
}
