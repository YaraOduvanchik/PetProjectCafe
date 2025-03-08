using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetProjectCafe.Application.Abstractions;
using PetProjectCafe.Domain.Orders.ValueObjects;

namespace PetProjectCafe.Application.OrderFeatures.Commands.RemoveMenuItem;

public class RemoveOrderItemHandler
{
    private readonly IOrderRepository _repository;
    private readonly ILogger<RemoveOrderItemHandler> _logger;

    public RemoveOrderItemHandler(IOrderRepository repository, ILogger<RemoveOrderItemHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<UnitResult<string>> Handle(RemoveOrderItemCommand command, CancellationToken cancellationToken)
    {
        var orderResult = await _repository.GetById(command.OrderId, cancellationToken);
        if (orderResult.IsFailure)
            return Result.Failure(orderResult.Error);

        if (orderResult.Value.Status != OrderStatus.AtWork)
            return Result.Failure("You cannot change the list of items for an order that is not in progress.");

        var result = orderResult.Value.RemoveOrderItem(command.OrderItemId);
        if (result.IsFailure)
            return Result.Failure(result.Error);

        await _repository.SaveChanges(cancellationToken);

        _logger.LogInformation("Order item removed");

        return UnitResult.Success<string>();
    }
}