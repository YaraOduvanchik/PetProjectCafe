using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetProjectCafe.Application.Abstractions;
using PetProjectCafe.Domain.Menus;
using PetProjectCafe.Domain.ValueObjects;

namespace PetProjectCafe.Application.MenuFeatures.Commands.CreateMenuItem;

public class CreateMenuItemHandler
{
    private readonly IMenuRepository _repository;
    private readonly ILogger<CreateMenuItemHandler> _logger;

    public CreateMenuItemHandler(IMenuRepository repository, ILogger<CreateMenuItemHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<Guid>> Handle(CreateMenuItemCommand command, CancellationToken cancellationToken)
    {
        var menuResult = await _repository.GetById(command.MenuId, cancellationToken);
        if (menuResult.IsFailure)
            return Result.Failure<Guid>(menuResult.Error);

        var nameResult = Name.Create(command.Name);
        if (nameResult.IsFailure)
            return Result.Failure<Guid>(nameResult.Error);

        var menuItem = MenuItem.Create(nameResult.Value, command.Price);
        if (menuItem.IsFailure)
            return Result.Failure<Guid>(menuItem.Error);

        var result = menuResult.Value.AddMenuItem(menuItem.Value);
        if (result.IsFailure)
            return Result.Failure<Guid>(result.Error);

        await _repository.SaveChanges(cancellationToken);

        _logger.LogInformation("Menu item added");

        return menuItem.Value.Id.Value;
    }
}