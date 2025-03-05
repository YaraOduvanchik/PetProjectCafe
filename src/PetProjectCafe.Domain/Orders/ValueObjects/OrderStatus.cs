using CSharpFunctionalExtensions;

namespace PetProjectCafe.Domain.Orders.ValueObjects;

public sealed class OrderStatus : ComparableValueObject
{
    public static readonly OrderStatus AtWork = new(nameof(AtWork));
    public static readonly OrderStatus Completed = new(nameof(Completed));
    public static readonly OrderStatus Cancelled = new(nameof(Cancelled));
    
    private static readonly OrderStatus[] _orderItems = [AtWork, Completed, Cancelled];

    private OrderStatus(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<OrderStatus> Create(string value)
    {
        if (_orderItems.Any(o => o.Value.ToLower() == value.Trim().ToLower() == false))
        {
            return Result.Failure<OrderStatus>("Order status is not valid! Please choose a valid order status! (AtWork, Completed, Cancelled)");
        }
        
        return new OrderStatus(value);
    }

    protected override IEnumerable<IComparable> GetComparableEqualityComponents()
    {
        throw new NotImplementedException();
    }
}