using CSharpFunctionalExtensions;
using PetProjectCafe.Domain.Menu;
using PetProjectCafe.Domain.ValueObjects.Ids;

namespace PetProjectCafe.Domain.Orders;

public sealed class OrderItem : Entity<OrderItemId>
{
    public OrderItem(OrderItemId id, MenuItem menuItem) : base(id)
    {
        MenuItem = menuItem;
    }

    public MenuItem MenuItem { get; private set; }
}