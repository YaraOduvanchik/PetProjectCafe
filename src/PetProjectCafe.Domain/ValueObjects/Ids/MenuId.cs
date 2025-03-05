using CSharpFunctionalExtensions;

namespace PetProjectCafe.Domain.ValueObjects.Ids;

public sealed class MenuId : ComparableValueObject
{
    private MenuId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static MenuId NewId() => new(Guid.NewGuid());

    public static MenuId Empty() => new(Guid.Empty);

    public static MenuId Create(Guid id) => new(id);

    public static implicit operator MenuId(Guid id) => new(id);

    public static implicit operator Guid(MenuId menuId)
    {
        ArgumentNullException.ThrowIfNull(menuId);
        return menuId.Value;
    }

    protected override IEnumerable<IComparable> GetComparableEqualityComponents()
    {
        yield return Value;
    }
}