using FTS.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FTS.Infrastructure.DAL.Configurations;

internal class IngredientConfiguration : IEntityTypeConfiguration<Ingredient>
{
    public void Configure(EntityTypeBuilder<Ingredient> builder)
    {
        builder.HasKey(i => i.Id);

        builder.Property(i => i.Name)
            .IsRequired();

        builder.HasIndex(i => i.Name)
            .IsUnique();

        builder.Property(i => i.Barcode)
            .HasMaxLength(20);

        builder.Property(i => i.Basis)
            .HasConversion<string>()
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(i => i.DensityGPerMl)
            .HasPrecision(6, 4);

        builder.Property(i => i.GramsPerPiece)
            .HasPrecision(8, 3);

        builder.Property(i => i.Calories)
            .HasPrecision(8, 2)
            .IsRequired();
        builder.Property(i => i.Proteins)
            .HasPrecision(5, 2)
            .IsRequired();
        builder.Property(i => i.Fat)
            .HasPrecision(5, 2)
            .IsRequired();
        builder.Property(i => i.Carbohydrates)
            .HasPrecision(5, 2)
            .IsRequired();

        builder.Property(i => i.Sugars)
            .HasPrecision(5, 2);
        builder.Property(i => i.SaturatedFat)
            .HasPrecision(5, 2);
        builder.Property(i => i.Fiber)
            .HasPrecision(5, 2);
        builder.Property(i => i.Salt)
            .HasPrecision(5, 2);
    }
}