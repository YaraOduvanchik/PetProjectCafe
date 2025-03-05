using CSharpFunctionalExtensions;

namespace PetProjectCafe.Domain.ValueObjects.Ids;

public sealed class OrderId : ComparableValueObject
{
    private OrderId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static OrderId NewId() => new(Guid.NewGuid());

    public static OrderId Empty() => new(Guid.Empty);

    public static OrderId Create(Guid id) => new(id);

    public static implicit operator OrderId(Guid id) => new(id);

    public static implicit operator Guid(OrderId orderId)
    {
        ArgumentNullException.ThrowIfNull(orderId);
        return orderId.Value;
    }

    protected override IEnumerable<IComparable> GetComparableEqualityComponents()
    {
        yield return Value;
    }
}