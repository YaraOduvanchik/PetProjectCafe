using CSharpFunctionalExtensions;

namespace PetProjectCafe.Domain.ValueObjects;

public sealed class Name : ComparableValueObject
{
    private Name(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<Name> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Result.Failure<Name>("Name cannot be empty!");

        return new Name(value);
    }

    protected override IEnumerable<IComparable> GetComparableEqualityComponents()
    {
        yield return Value;
    }
}