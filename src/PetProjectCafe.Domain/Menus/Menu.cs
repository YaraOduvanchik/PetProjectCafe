using CSharpFunctionalExtensions;
using PetProjectCafe.Domain.ValueObjects;
using PetProjectCafe.Domain.ValueObjects.Ids;

namespace PetProjectCafe.Domain.Menus;

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

    public Result<MenuItem> GetMenuItemById(MenuItemId id)
    {
        var menuItem = _menuItems.SingleOrDefault(mi => mi.Id == id);
        if (menuItem is null)
            return Result.Failure<MenuItem>("Menu item not found!");

        return menuItem;
    }

    public UnitResult<string> AddMenuItem(MenuItem menuItem)
    {
        if (_menuItems.Any(mi => mi.Name == menuItem.Name))
        {
            return UnitResult.Failure<string>("Menu item with the same name already exists!");
        }

        _menuItems.Add(menuItem);

        return UnitResult.Success<string>();
    }
    
    public UnitResult<string> RemoveMenuItem(MenuItemId id)
    {
        var menuItemResult = GetMenuItemById(id);
        if (menuItemResult.IsFailure)
            return UnitResult.Failure(menuItemResult.Error);

        _menuItems.Remove(menuItemResult.Value);

        return UnitResult.Success<string>(); 
    }
}