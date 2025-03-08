using FluentAssertions;
using PetProjectCafe.Domain.Menus;
using PetProjectCafe.Domain.ValueObjects;
using PetProjectCafe.Domain.ValueObjects.Ids;

namespace PetProjectCafe.UnitTests.Domain;

public class MenuTests
{
    [Fact]
    public void GetMenuItemById_ExistingMenuItem_ReturnsMenuItem()
    {
        // Arrange
        var menuId = MenuId.NewId();
        var menu = new Menu(menuId, Name.Create("Test").Value);
        menu.AddMenuItem(MenuItem.Create(Name.Create("Test").Value, 10).Value);
        
        var menuItemId = menu.MenuItems.First().Id;

        // Act
        var result = menu.GetMenuItemById(menuItemId);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Id.Should().Be(menuItemId);
    }

    [Fact]
    public void GetMenuItemById_NonExistingMenuItem_ReturnsFailure()
    {
        // Arrange
        var menuId = MenuId.NewId();
        var menu = new Menu(menuId, Name.Create("Test").Value);

        // Act
        var result = menu.GetMenuItemById(MenuItemId.NewId());

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be("Menu item not found!");
    }

    [Fact]
    public void AddMenuItem_MenuItemWithSameName_ReturnsFailure()
    {
        // Arrange
        var menuId = MenuId.NewId();
        var menu = new Menu(menuId, Name.Create("Test").Value);
        menu.AddMenuItem(MenuItem.Create(Name.Create("Test").Value, 10).Value);

        // Act
        var result = menu.AddMenuItem(MenuItem.Create(Name.Create("Test").Value, 10).Value);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be("Menu item with the same name already exists!");
    }

    [Fact]
    public void AddMenuItem_MenuItemWithDifferentName_ReturnsSuccess()
    {
        // Arrange
        var menuId = MenuId.NewId();
        var menu = new Menu(menuId, Name.Create("Test").Value);

        // Act
        var result = menu.AddMenuItem(MenuItem.Create(Name.Create("Test1").Value, 10).Value);

        // Assert
        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public void RemoveMenuItem_ExistingMenuItem_ReturnsSuccess()
    {
        // Arrange
        var menuId = MenuId.NewId();
        var menu = new Menu(menuId, Name.Create("Test").Value);
        menu.AddMenuItem(MenuItem.Create(Name.Create("Test").Value, 10).Value);

        var menuItemId = menu.MenuItems.First().Id;

        // Act
        var result = menu.RemoveMenuItem(menuItemId);

        // Assert
        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public void RemoveMenuItem_NonExistingMenuItem_ReturnsFailure()
    {
        // Arrange
        var menuId = MenuId.NewId();
        var menu = new Menu(menuId, Name.Create("Test").Value);

        // Act
        var result = menu.RemoveMenuItem(MenuItemId.NewId());

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be("Menu item not found!");
    }
}