using FTS.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FTS.Infrastructure.DAL.Configurations;

internal class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);
        builder.Property(u => u.Name)
            .IsRequired();
        builder.Property(u => u.Password)
            .IsRequired();
        builder.HasIndex(u => u.Email)
            .IsUnique();
        builder.Property(u => u.Role)
            .IsRequired();
    }
}