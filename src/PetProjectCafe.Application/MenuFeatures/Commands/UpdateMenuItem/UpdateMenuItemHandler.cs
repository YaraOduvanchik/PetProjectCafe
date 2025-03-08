using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetProjectCafe.Application.Abstractions;
using PetProjectCafe.Domain.ValueObjects;

namespace PetProjectCafe.Application.MenuFeatures.Commands.UpdateMenuItem;

public class UpdateMenuItemHandler
{
    private readonly IMenuRepository _repository;
    private readonly ILogger<UpdateMenuItemHandler> _logger;

    public UpdateMenuItemHandler(IMenuRepository repository, ILogger<UpdateMenuItemHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<Guid>> Handle(UpdateMenuItemCommand command, CancellationToken cancellationToken)
    {
        var menuResult = await _repository.GetById(command.MenuId, cancellationToken);
        if (menuResult.IsFailure)
            return Result.Failure<Guid>(menuResult.Error);

        var nameResult = Name.Create(command.Name);
        if (nameResult.IsFailure)
            return Result.Failure<Guid>(nameResult.Error);

        if (menuResult.Value.MenuItems.Any(mi => mi.Name == nameResult.Value))
        {
            return Result.Failure<Guid>("Menu item with the same name already exists!");
        }

        var menuItemResult = menuResult.Value.GetMenuItemById(command.MenuItemId);
        if (menuItemResult.IsFailure)
            return Result.Failure<Guid>(menuItemResult.Error);

        menuItemResult.Value.UpdateFullInfo(nameResult.Value, command.Price);

        await _repository.SaveChanges(cancellationToken);

        _logger.LogInformation("Menu item updated");

        return menuItemResult.Value.Id.Value;
    }
}