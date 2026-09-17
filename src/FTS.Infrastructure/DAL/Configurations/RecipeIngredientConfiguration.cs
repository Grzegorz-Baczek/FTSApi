using FTS.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FTS.Infrastructure.DAL.Configurations;

internal class RecipeIngredientConfiguration : IEntityTypeConfiguration<RecipeIngredient>
{
    public void Configure(EntityTypeBuilder<RecipeIngredient> builder)
    {
        builder.HasKey(ri => ri.Id);

        builder.Property(ri => ri.AmountInGrams)
            .HasPrecision(10, 3)
            .IsRequired();
        builder.Property(ri => ri.Unit)
            .HasConversion<string>()
            .HasMaxLength(20)
            .IsRequired();

        builder.HasOne(ri => ri.Recipe)
            .WithMany(r => r.RecipeIngredients)
            .HasForeignKey(ri => ri.RecipeId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(ri => ri.Ingredient)
            .WithMany(i => i.RecipeIngredients)
            .HasForeignKey(ri => ri.IngredientId)
            .OnDelete(DeleteBehavior.Restrict); // nie usuwamy składników globalnych

        builder.HasIndex(ri => new { ri.RecipeId, ri.IngredientId })
            .IsUnique();
    }
}