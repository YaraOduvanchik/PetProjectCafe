using CSharpFunctionalExtensions;
using PetProjectCafe.Domain.ValueObjects;
using PetProjectCafe.Domain.ValueObjects.Ids;

namespace PetProjectCafe.Domain.Menu;

public sealed class Menu : Entity<MenuId>
{
    private readonly List<MenuItem> _menuItems = [];

    // Ef core constructor
    private Menu(MenuId id) : base(id)
    {
    }

    public Menu(MenuId id, Name name) : base(id)
    {
        Name = name;
    }

    public Name Name { get; private set; }

    public IReadOnlyCollection<MenuItem> MenuItems => _menuItems.AsReadOnly();

    public UnitResult<string> AddMenuItem(MenuItem menuItem)
    {
        if (_menuItems.Any(mi => mi.Name == menuItem.Name))
        {
            return UnitResult.Failure<string>("Menu item with the same name already exists!");
        }

        _menuItems.Add(menuItem);

        return UnitResult.Success<string>();
    }

    public void RemoveMenuItem(MenuItem menuItem) => _menuItems.Remove(menuItem);
}