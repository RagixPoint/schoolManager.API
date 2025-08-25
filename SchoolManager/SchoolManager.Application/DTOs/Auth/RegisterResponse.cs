using System;

namespace SchoolManager.Application.DTOs.Auth;

public class RegisterResponse
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public Guid? UserId { get; set; }

}