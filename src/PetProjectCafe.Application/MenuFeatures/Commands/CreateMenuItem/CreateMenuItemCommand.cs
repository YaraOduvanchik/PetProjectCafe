namespace PetProjectCafe.Application.MenuFeatures.Commands.CreateMenuItem;

public record CreateMenuItemCommand(Guid MenuId, string Name, decimal Price);