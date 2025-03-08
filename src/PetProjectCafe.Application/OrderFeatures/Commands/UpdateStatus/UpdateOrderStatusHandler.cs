using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetProjectCafe.Application.Abstractions;
using PetProjectCafe.Domain.Orders.ValueObjects;

namespace PetProjectCafe.Application.OrderFeatures.Commands.UpdateStatus;

public class UpdateOrderStatusHandler
{
    private readonly IOrderRepository _repository;
    private readonly ILogger<UpdateOrderStatusHandler> _logger;

    public UpdateOrderStatusHandler(IOrderRepository repository, ILogger<UpdateOrderStatusHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<UnitResult<string>> Handle(UpdateOrderStatusCommand command, CancellationToken cancellationToken)
    {
        var orderResult = await _repository.GetById(command.OrderId, cancellationToken);
        if (orderResult.IsFailure)
            return Result.Failure<Guid>(orderResult.Error);

        var statusResult = OrderStatus.Create(command.Status);
        if (statusResult.IsFailure)
            return Result.Failure<Guid>(statusResult.Error);

        var result = orderResult.Value.UpdateStatus(statusResult.Value);
        if (result.IsFailure)
            return Result.Failure<Guid>(result.Error);

        await _repository.SaveChanges(cancellationToken);

        _logger.LogInformation("Order status updated");

        return UnitResult.Success<string>();
    }
}