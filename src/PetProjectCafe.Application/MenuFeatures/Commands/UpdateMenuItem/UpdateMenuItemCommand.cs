namespace PetProjectCafe.Application.MenuFeatures.Commands.UpdateMenuItem;

public record UpdateMenuItemCommand(Guid MenuId, Guid MenuItemId, string Name, decimal Price);