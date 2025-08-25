using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolManager.Application.DTOs.Auth;
using SchoolManager.Application.DTOs.User;
using SchoolManager.Application.Interfaces;
using SchoolManager.Application.Interfaces.Auth;
using SchoolManager.Domain.Entities;
using SchoolManager.Domain.Enums;

namespace SchoolManager.Application.Features.Auth.Commands;

public sealed record RegisterCommand(
    string Email,
    string Password,
    string ConfirmPassword,
    string FirstName,
    string LastName,
    UserRole Role
) : IRequest<RegisterResponse>;

public class RegisterCommandHandler(IAppDbContext context, IJwtService jwtService)
    : IRequestHandler<RegisterCommand, RegisterResponse>
{
    public async Task<RegisterResponse> Handle( RegisterCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken: cancellationToken);
            if (user != null)
                throw new Exception("Email already exists");
            var createdUser = context.Users.Add(new User()
            {
                Email = request.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                FirstName = request.FirstName,
                LastName = request.LastName,
                Role = request.Role,
                IsActive = true
            });
			await context.SaveChangesAsync(cancellationToken);
            return new RegisterResponse()
            {
                Success = true,
                Message = "User created successfully",
                UserId = createdUser.Entity.Id
            };
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}