using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetProjectCafe.Application.Abstractions;
using PetProjectCafe.Domain.Menus;
using PetProjectCafe.Domain.ValueObjects;
using PetProjectCafe.Domain.ValueObjects.Ids;

namespace PetProjectCafe.Application.MenuFeatures.Commands.Create;

public class CreateMenuHandler
{
    private readonly IMenuRepository _repository;
    private readonly ILogger<CreateMenuHandler> _logger;

    public CreateMenuHandler(IMenuRepository repository, ILogger<CreateMenuHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }
    
    public async Task<Result<Guid>> Handle(CreateMenuCommand command, CancellationToken cancellationToken)
    {
        var nameResult = Name.Create(command.Name);
        if (nameResult.IsFailure)
            return Result.Failure<Guid>(nameResult.Error);

        var menu = new Menu(MenuId.NewId(), nameResult.Value);
        
        await _repository.Create(menu, cancellationToken);

        await _repository.SaveChanges(cancellationToken);
        
        _logger.LogInformation("Menu created");
        
        return menu.Id.Value;
    }
}