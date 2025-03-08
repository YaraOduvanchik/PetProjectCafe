using CSharpFunctionalExtensions;
using PetProjectCafe.Domain.ValueObjects.Ids;

namespace PetProjectCafe.Domain.Orders;

public sealed class OrderItem : Entity<OrderItemId>
{
    // Ef core constructor
    private OrderItem(OrderItemId id) : base(id)
    {
    }

    private OrderItem(
        OrderItemId id,
        MenuItemId menuItemId,
        int quantity)
        : base(id)
    {
        MenuItemId = menuItemId;
        Quantity = quantity;
    }

    public MenuItemId MenuItemId { get; private set; }

    public int Quantity { get; private set; }

    public OrderId OrderId { get; private set; }

    public static Result<OrderItem> Create(MenuItemId menuItemId, int quantity)
    {
        if (quantity < 1)
            return Result.Failure<OrderItem>("Quantity cannot be less than 1!");

        return new OrderItem(OrderItemId.NewId(), menuItemId, quantity);
    }

    public void UpdateFullInfo(MenuItemId menuItemId, int quantity) => (MenuItemId, Quantity) = (menuItemId, quantity);
}