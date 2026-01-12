using FTS.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FTS.Infrastructure.DAL.Configurations;

internal class CookbookRecipeConfiguration : IEntityTypeConfiguration<CookbookRecipe>
{
    public void Configure(EntityTypeBuilder<CookbookRecipe> builder)
    {
        builder.HasKey(cr => cr.Id);

        builder.HasIndex(cr => new { cr.CookbookId, cr.RecipeId })
            .IsUnique();

        builder.HasOne(cr => cr.Cookbook)
            .WithMany(c => c.CookbookRecipes)
            .HasForeignKey(cr => cr.CookbookId)
            .OnDelete(DeleteBehavior.Cascade);
        //nie mozna usunac przepisu dopóki, przepis nie zostanie odpiety od każdej ksiazki z przepisami, w ktorych zostal dodany
        builder.HasOne(cr => cr.Recipe)
            .WithMany(r => r.CookbookRecipes)
            .HasForeignKey(cr => cr.RecipeId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}