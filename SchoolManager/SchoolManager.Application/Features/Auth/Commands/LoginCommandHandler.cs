using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolManager.Application.DTOs.Auth;
using SchoolManager.Application.DTOs.User;
using SchoolManager.Application.Interfaces;
using SchoolManager.Application.Interfaces.Auth;

namespace SchoolManager.Application.Features.Auth.Commands;

public sealed record LoginCommand(string Email, string Password) : IRequest<AuthResponse?>;

public class LoginCommandHandler(IAppDbContext context, IJwtService jwtService)
    : IRequestHandler<LoginCommand, AuthResponse?>
{
    public async Task<AuthResponse?> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await context.Users.FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken: cancellationToken);

        if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
        {
            return null;
        }

        if (!user.IsActive)
        {
            return null;
        }

        var token = jwtService.GenerateToken(user);

        return new AuthResponse
        {
            Token = token,
            User = new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Role = user.Role.ToString()
            }
        };
    }
}