using CSharpFunctionalExtensions;

namespace PetProjectCafe.Domain.ValueObjects.Ids;

public sealed class MenuItemId : ComparableValueObject
{
    private MenuItemId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static MenuItemId NewId() => new(Guid.NewGuid());

    public static MenuItemId Empty() => new(Guid.Empty);

    public static MenuItemId Create(Guid id) => new(id);

    public static implicit operator MenuItemId(Guid id) => new(id);

    public static implicit operator Guid(MenuItemId menuItemId)
    {
        ArgumentNullException.ThrowIfNull(menuItemId);
        return menuItemId.Value;
    }

    protected override IEnumerable<IComparable> GetComparableEqualityComponents()
    {
        yield return Value;
    }
}