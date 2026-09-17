using FTS.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FTS.Infrastructure.DAL.Configurations;

internal class RecipeConfiguration : IEntityTypeConfiguration<Recipe>
{
    public void Configure(EntityTypeBuilder<Recipe> builder)
    {
        builder.HasKey(r => r.Id);

        builder.Property(r => r.Title)
            .IsRequired();

        builder.Property(r => r.Steps)
            .IsRequired();

        builder.Property(r => r.Servings)
            .IsRequired();

        builder.Property(r => r.KcalTotal)
            .HasPrecision(10, 2)
            .IsRequired();
        builder.Property(r => r.KcalPerServing)
            .HasPrecision(10, 2)
            .IsRequired();
        builder.Property(r => r.CarbohydratesTotal)
            .HasPrecision(10, 3)
            .IsRequired();
        builder.Property(r => r.ProteinsTotal)
            .HasPrecision(10, 3)
            .IsRequired();
        builder.Property(r => r.FatTotal)
            .HasPrecision(10, 3)
            .IsRequired();

        builder.HasOne(r => r.Author)
          .WithMany(u => u.Recipes)
          .HasForeignKey(r => r.AuthorId)
          .OnDelete(DeleteBehavior.Restrict);
    }
}