using SchoolManager.Application.DTOs.User;

namespace SchoolManager.Application.DTOs.Auth;
public class AuthResponse
{
    public string Token { get; set; } = string.Empty;
    public UserDto User { get; set; } = null!;
}