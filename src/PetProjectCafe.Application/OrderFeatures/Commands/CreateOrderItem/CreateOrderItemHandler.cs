using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetProjectCafe.Application.Abstractions;
using PetProjectCafe.Domain.Orders;
using PetProjectCafe.Domain.Orders.ValueObjects;

namespace PetProjectCafe.Application.OrderFeatures.Commands.CreateOrderItem;

public class CreateOrderItemHandler
{
    private readonly IOrderRepository _repository;
    private readonly ILogger<CreateOrderItemHandler> _logger;

    public CreateOrderItemHandler(IOrderRepository repository, ILogger<CreateOrderItemHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<Guid>> Handle(CreateOrderItemCommand command, CancellationToken cancellationToken)
    {
        var orderResult = await _repository.GetById(command.OrderId, cancellationToken);
        if (orderResult.IsFailure)
            return Result.Failure<Guid>(orderResult.Error);

        if (orderResult.Value.Status != OrderStatus.AtWork)
            return Result.Failure<Guid>("You cannot change the list of items for an order that is not in progress.");

        var orderItemResult = OrderItem.Create(command.MenuItemId, command.Quantity);
        if (orderItemResult.IsFailure)
            return Result.Failure<Guid>(orderItemResult.Error);

        orderResult.Value.AddOrderItem(orderItemResult.Value);

        await _repository.SaveChanges(cancellationToken);

        _logger.LogInformation("Order item added");

        return orderItemResult.Value.Id.Value;
    }
}