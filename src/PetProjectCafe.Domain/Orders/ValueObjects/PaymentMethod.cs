using CSharpFunctionalExtensions;

namespace PetProjectCafe.Domain.Orders.ValueObjects;

public sealed class PaymentMethod : ComparableValueObject
{
    public static readonly PaymentMethod Cash = new(nameof(Cash));
    public static readonly PaymentMethod Card = new(nameof(Card));

    private static readonly PaymentMethod[] _paymentMethods = [Cash, Card];

    private PaymentMethod(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<PaymentMethod> Create(string value)
    {
        if (_paymentMethods.Any(p => p.Value.ToLower() == value.Trim().ToLower()) == false)
        {
            return Result.Failure<PaymentMethod>("Payment method is not valid! Please choose a valid payment method! (Cash or Card)");
        }

        return new PaymentMethod(value);
    }
    
    protected override IEnumerable<IComparable> GetComparableEqualityComponents()
    {
        yield return Value;
    }
}