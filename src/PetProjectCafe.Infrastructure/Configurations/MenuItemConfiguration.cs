using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetProjectCafe.Domain.Menus;
using PetProjectCafe.Domain.ValueObjects.Ids;

namespace PetProjectCafe.Infrastructure.Configurations;

public class MenuItemConfiguration : IEntityTypeConfiguration<MenuItem>
{
    public void Configure(EntityTypeBuilder<MenuItem> builder)
    {
        builder.ToTable("menu_items");

        builder.HasKey(mi => mi.Id);

        builder.Property(mi => mi.Id)
            .HasConversion(
                id => id.Value,
                value => MenuItemId.Create(value));
        
        builder.ComplexProperty(m => m.Name, nb =>
        {
            nb.Property(n => n.Value)
                .IsRequired();
        });

        builder.Property(mi => mi.Price).IsRequired();
    }
}