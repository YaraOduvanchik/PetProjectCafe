using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetProjectCafe.Domain.Orders;
using PetProjectCafe.Domain.ValueObjects.Ids;

namespace PetProjectCafe.Infrastructure.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("orders");

        builder.HasKey(o => o.Id);

        builder.Property(o => o.Id)
            .HasConversion(
                id => id.Value,
                value => OrderId.Create(value));

        builder.ComplexProperty(o => o.ClientName, cb =>
        {
            cb.Property(n => n.Value).IsRequired();
        });

        builder.Property(o => o.DateAndTime).IsRequired();
        
        builder.ComplexProperty(o => o.PaymentMethod, cb =>
        {
            cb.Property(n => n.Value).IsRequired();
        });
        
        builder.ComplexProperty(o => o.Status, cb =>
        {
            cb.Property(n => n.Value).IsRequired();
        });
        
        builder.HasMany(o => o.OrderItems)
            .WithOne()
            .HasForeignKey(oi => oi.OrderId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}