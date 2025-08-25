using SchoolManager.Domain.Enums;

namespace SchoolManager.Domain.Entities;

public class User : BaseEntity
{
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public UserRole Role { get; set; } = UserRole.Parent;
    public bool IsActive { get; set; } = true;

    // Domain methods
    public bool IsEmailValid()
    {
        return Email.Contains('@') && Email.Contains('.');
    }

    public string GetFullName()
    {
        return $"{FirstName} {LastName}".Trim();
    }
}