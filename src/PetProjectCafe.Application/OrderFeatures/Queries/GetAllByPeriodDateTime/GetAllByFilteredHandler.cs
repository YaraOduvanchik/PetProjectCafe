using Microsoft.Extensions.Logging;
using PetProjectCafe.Application.Abstractions;
using PetProjectCafe.Application.DTOs;

namespace PetProjectCafe.Application.OrderFeatures.Queries.GetAllByPeriodDateTime;

public class GetAllByFilteredHandler
{
    private readonly IOrderRepository _repository;
    private readonly ILogger<GetAllByFilteredHandler> _logger;

    public GetAllByFilteredHandler(IOrderRepository repository, ILogger<GetAllByFilteredHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<IReadOnlyCollection<OrderDto>> Handle(
        GetAllByFilteredCommand command,
        CancellationToken cancellationToken)
    {
        var orders = await _repository.GetByPeriodDateTimeAndStatus(
            command.TimeIntervalFrom,
            command.TimeIntervalTo,
            command.Status,
            cancellationToken);

        var ordersDto = orders
            .Select(o => new OrderDto(
                o.Id.Value,
                o.ClientName.Value,
                o.DateAndTime,
                o.PaymentMethod.Value,
                o.Status.Value,
                o.OrderItems.Select(oi => new OrderItemDto(oi.MenuItemId.Value, oi.Quantity)).ToList())
            ).ToList();

        _logger.LogInformation("Menu item updated");

        return ordersDto;
    }
}