using System;
using System.Collections.Generic;

namespace SchoolManager.Domain.Entities;

public class Student : BaseEntity
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public string StudentNumber { get; set; } = string.Empty;

    // Domain methods
    public int GetAge()
    {
        var today = DateTime.Today;
        var age = today.Year - DateOfBirth.Year;
        if (DateOfBirth > today.AddYears(-age)) age--;
        return age;
    }
}