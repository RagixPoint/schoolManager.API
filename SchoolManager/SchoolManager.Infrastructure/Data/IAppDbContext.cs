using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SchoolManager.Domain.Entities;

namespace SchoolManager.Application.Interfaces;

public interface IAppDbContext
{
    DbSet<Student> Students { get; set; }
    DbSet<User> Users { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    
}