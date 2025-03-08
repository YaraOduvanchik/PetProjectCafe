using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetProjectCafe.Application.Abstractions;
using PetProjectCafe.Domain.Orders;
using PetProjectCafe.Domain.Orders.ValueObjects;
using PetProjectCafe.Domain.ValueObjects;
using PetProjectCafe.Domain.ValueObjects.Ids;

namespace PetProjectCafe.Application.OrderFeatures.Commands.Create;

public class CreateOrderHandler
{
    private readonly IOrderRepository _repository;
    private readonly ILogger<CreateOrderHandler> _logger;

    public CreateOrderHandler(IOrderRepository repository, ILogger<CreateOrderHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<Guid>> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        var nameResult = Name.Create(command.Name);
        if (nameResult.IsFailure)
            return Result.Failure<Guid>(nameResult.Error);

        var paymentMethodResult = PaymentMethod.Create(command.PaymentMethod);
        if (paymentMethodResult.IsFailure)
            return Result.Failure<Guid>(paymentMethodResult.Error);

        var order = new Order(OrderId.NewId(), nameResult.Value, paymentMethodResult.Value);
        
        await _repository.Create(order, cancellationToken);

        List<OrderItem> orderItems = [];

        foreach (var orderItem in command.OrderItems)
        {
            var orderItemResult = OrderItem.Create(orderItem.MenuItemId, orderItem.Quantity);
            if (orderItemResult.IsFailure)
                return Result.Failure<Guid>(orderItemResult.Error);

            orderItems.Add(orderItemResult.Value);
        }

        order.AddOrderItems(orderItems);

        await _repository.SaveChanges(cancellationToken);

        _logger.LogInformation("Order created");

        return order.Id.Value;
    }
}