using CSharpFunctionalExtensions;
using PetProjectCafe.Domain.Menu;
using PetProjectCafe.Domain.ValueObjects.Ids;

namespace PetProjectCafe.Domain.Orders;

public sealed class OrderItem : Entity<OrderItemId>
{
    // Ef core constructor
    private OrderItem(OrderItemId id) : base(id)
    {
    }
    
    public OrderItem(OrderItemId id, MenuItemId menuItemId) : base(id)
    {
        MenuItemId = menuItemId;
    }

    public MenuItemId MenuItemId { get; private set; }

    public OrderId OrderId { get; private set; }
}