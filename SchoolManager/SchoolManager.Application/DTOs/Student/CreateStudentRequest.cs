using System;

namespace SchoolManager.Application.DTOs.Student;

public class CreateStudentRequest
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public string StudentNumber { get; set; } = string.Empty;
}
