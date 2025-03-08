using CSharpFunctionalExtensions;
using PetProjectCafe.Domain.ValueObjects;
using PetProjectCafe.Domain.ValueObjects.Ids;

namespace PetProjectCafe.Domain.Menus;

public sealed class MenuItem : Entity<MenuItemId>
{
    // Ef core constructor
    private MenuItem(MenuItemId id) : base(id)
    {
    }

    private MenuItem(MenuItemId id, Name name, decimal price) : base(id)
    {
        Name = name;
        Price = price;
    }

    public Name Name { get; private set; }

    public decimal Price { get; private set; }

    public MenuId MenuId { get; private set; }

    public static Result<MenuItem> Create(Name name, decimal price)
    {
        if (price < 0)
            return Result.Failure<MenuItem>("Price cannot be negative!");

        return new MenuItem(MenuItemId.NewId(), name, price);
    }

    public void UpdateFullInfo(Name name, decimal price) => (Name, Price) = (name, price);
}