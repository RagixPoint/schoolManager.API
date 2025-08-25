using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolManager.Application.Interfaces;
using SchoolManager.Domain.Entities;

namespace SchoolManager.Infrastructure.Data;

public sealed class AppDbContext(DbContextOptions<AppDbContext> options)
    : DbContext(options), IAppDbContext
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Appliquer les configurations depuis les fichiers de configuration
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }

    public override int SaveChanges()
    {
        UpdateTimestamps();
        return base.SaveChanges();
    }
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateTimestamps();
        return await base.SaveChangesAsync(cancellationToken);
    }
    private void UpdateTimestamps()
    {
        var entities = ChangeTracker.Entries()
            .Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));

        foreach (var entity in entities)
        {
            var baseEntity = (BaseEntity)entity.Entity;
            
            if (entity.State == EntityState.Added)
            {
                baseEntity.CreatedAt = DateTime.UtcNow;
            }
            
            baseEntity.UpdatedAt = DateTime.UtcNow;
        }
    }


    public DbSet<Student> Students { get; set; }
    public DbSet<User> Users { get; set; }
}