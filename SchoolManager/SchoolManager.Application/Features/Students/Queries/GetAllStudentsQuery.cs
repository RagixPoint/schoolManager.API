using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolManager.Application.DTOs;
using SchoolManager.Application.DTOs.Student;
using SchoolManager.Application.Interfaces;

namespace SchoolManager.Application.Features.Students.Queries;

public record GetAllStudentsQuery : IRequest<List<StudentDto>>;

public class GetAllStudentsQueryHandler(IAppDbContext context) : IRequestHandler<GetAllStudentsQuery, List<StudentDto>>
{
    private readonly IAppDbContext _context = context;

    public async Task<List<StudentDto>> Handle(GetAllStudentsQuery request, CancellationToken cancellationToken)
    {
        var students = await _context.Students.ToListAsync(cancellationToken);
        
        return students.Select(s => new StudentDto
        {
            Id = s.Id,
            FirstName = s.FirstName,
            LastName = s.LastName,
            Email = s.Email,
            DateOfBirth = s.DateOfBirth,
            StudentNumber = s.StudentNumber
        }).ToList();
    }
}