using FTS.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FTS.Infrastructure.DAL.Configurations;

internal class PointsLogConfiguration : IEntityTypeConfiguration<PointsLog>
{
    public void Configure(EntityTypeBuilder<PointsLog> builder)
    {
        builder.HasKey(c => c.Id);

        // Punkty powiązane z przepisem - najpierw musisz usunac punkty potem mozesz usunac przepis
        builder.HasOne(p => p.Recipe)
            .WithMany(r => r.PointsLogs)
            .HasForeignKey(p => p.RecipeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(p => p.User)
            .WithMany(u => u.PointsLogs)
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(p => p.UserId);
        builder.HasIndex(p => p.RecipeId);
    }
}