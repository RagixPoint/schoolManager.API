using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolManager.Domain.Entities;


namespace SchoolManager.Infrastructure.Data.ETC.User;

public class UserEtc : IEntityTypeConfiguration<Domain.Entities.User>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.User> builder)
    {
        builder.HasKey(e => e.Id);
        builder.HasIndex(e => e.Email).IsUnique();
        builder.Property(e => e.Email).IsRequired().HasMaxLength(255);
        builder.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
        builder.Property(e => e.LastName).IsRequired().HasMaxLength(100);
        builder.Property(e => e.PasswordHash).IsRequired();
        builder.Property(e => e.Role).HasConversion<int>();
    }
}
