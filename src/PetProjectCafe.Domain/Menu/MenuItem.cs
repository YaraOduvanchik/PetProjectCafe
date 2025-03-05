using CSharpFunctionalExtensions;
using PetProjectCafe.Domain.ValueObjects;
using PetProjectCafe.Domain.ValueObjects.Ids;

namespace PetProjectCafe.Domain.Menu;

public sealed class MenuItem : Entity<MenuItemId>
{
    // Ef core constructor
    private MenuItem(MenuItemId id) : base(id)
    {
    }

    public MenuItem(MenuItemId id, Name name) : base(id)
    {
        Name = name;
    }

    public Name Name { get; private set; }

    public MenuId MenuId { get; private set; }

    public void UpdateName(Name name) => Name = name;
}