using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetProjectCafe.Domain.Menus;
using PetProjectCafe.Domain.ValueObjects.Ids;

namespace PetProjectCafe.Infrastructure.Configurations;

public class MenuConfiguration : IEntityTypeConfiguration<Menu>
{
    public void Configure(EntityTypeBuilder<Menu> builder)
    {
        builder.ToTable("menus");

        builder.HasKey(m => m.Id);

        builder.Property(m => m.Id)
            .HasConversion(
                id => id.Value,
                value => MenuId.Create(value));

        builder.ComplexProperty(m => m.Name, nb =>
        {
            nb.Property(n => n.Value).IsRequired();
        });

        builder.HasMany(m => m.MenuItems)
            .WithOne()
            .HasForeignKey(mi => mi.MenuId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}