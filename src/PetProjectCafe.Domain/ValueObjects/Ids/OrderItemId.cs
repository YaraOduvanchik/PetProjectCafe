using CSharpFunctionalExtensions;

namespace PetProjectCafe.Domain.ValueObjects.Ids;

public sealed class OrderItemId : ComparableValueObject
{
    private OrderItemId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static OrderItemId NewId() => new(Guid.NewGuid());

    public static OrderItemId Empty() => new(Guid.Empty);

    public static OrderItemId Create(Guid id) => new(id);

    public static implicit operator OrderItemId(Guid id) => new(id);

    public static implicit operator Guid(OrderItemId orderItemId)
    {
        ArgumentNullException.ThrowIfNull(orderItemId);
        return orderItemId.Value;
    }

    protected override IEnumerable<IComparable> GetComparableEqualityComponents()
    {
        yield return Value;
    }
}