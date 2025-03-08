using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetProjectCafe.Application.Abstractions;

namespace PetProjectCafe.Application.MenuFeatures.Commands.RemoveMenuItem;

public class RemoveMenuItemHandler
{
    private readonly IMenuRepository _repository;
    private readonly ILogger<RemoveMenuItemHandler> _logger;

    public RemoveMenuItemHandler(IMenuRepository repository, ILogger<RemoveMenuItemHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<UnitResult<string>> Handle(RemoveMenuItemCommand command, CancellationToken cancellationToken)
    {
        var menuResult = await _repository.GetById(command.MenuId, cancellationToken);
        if (menuResult.IsFailure)
            return Result.Failure<Guid>(menuResult.Error);
        
        var result = menuResult.Value.RemoveMenuItem(command.MenuItemId);
        if (result.IsFailure)
            return Result.Failure<Guid>(result.Error);

        await _repository.SaveChanges(cancellationToken);

        _logger.LogInformation("Menu item removed");

        return UnitResult.Success<string>();
    }
}