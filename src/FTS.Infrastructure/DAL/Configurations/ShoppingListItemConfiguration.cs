using FTS.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FTS.Infrastructure.DAL.Configurations;

internal sealed class ShoppingListItemConfiguration : IEntityTypeConfiguration<ShoppingListItem>
{
    public void Configure(EntityTypeBuilder<ShoppingListItem> builder)
    {
        builder.HasKey(i => i.Id);

        builder.Property(i => i.ProductName)
            .IsRequired()
            .HasMaxLength(300);

        builder.Property(i => i.Quantity)
            .HasPrecision(10, 3);

        builder.Property(i => i.Unit)
            .HasMaxLength(50);

        builder.Property(i => i.Category)
            .HasMaxLength(100);

        builder.Property(i => i.IsChecked)
            .IsRequired()
            .HasDefaultValue(false);
    }
}
