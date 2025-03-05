using CSharpFunctionalExtensions;
using PetProjectCafe.Domain.Orders.ValueObjects;
using PetProjectCafe.Domain.ValueObjects;
using PetProjectCafe.Domain.ValueObjects.Ids;

namespace PetProjectCafe.Domain.Orders;

public sealed class Order : Entity<OrderId>
{
    private readonly List<OrderItem> _orderItems = new();

    // Ef core constructor
    private Order(OrderId id) : base(id)
    {
    }

    private Order(
        OrderId id,
        Name clientName,
        PaymentMethod paymentMethod)
        : base(id)
    {
        ClientName = clientName;
        DateAndTime = DateTime.UtcNow;
        PaymentMethod = paymentMethod;
        Status = OrderStatus.AtWork;
    }

    public Name ClientName { get; private set; }

    public DateTime DateAndTime { get; private set; }

    public PaymentMethod PaymentMethod { get; private set; }

    public OrderStatus Status { get; private set; }

    public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();

    public UnitResult<string> UpdateStatus(OrderStatus status)
    {
        if (Status == OrderStatus.Completed && status == OrderStatus.Cancelled)
        {
            return UnitResult.Failure<string>("You cannot cancel a completed order!");
        }

        if (Status == OrderStatus.Cancelled && status == OrderStatus.Completed)
        {
            return UnitResult.Failure<string>("You cannot complete a cancelled order!");
        }
        
        Status = status;
        
        return UnitResult.Success<string>();
    }

    public void AddOrderItem(OrderItem orderItem) => _orderItems.Add(orderItem);

    public void RemoveOrderItem(OrderItem orderItem) => _orderItems.Remove(orderItem);
}