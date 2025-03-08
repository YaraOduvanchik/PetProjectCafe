using Microsoft.AspNetCore.Mvc;
using PetProjectCafe.API.Controllers.Requests.Orders;
using PetProjectCafe.Application.OrderFeatures.Commands.Create;
using PetProjectCafe.Application.OrderFeatures.Commands.CreateOrderItem;
using PetProjectCafe.Application.OrderFeatures.Commands.RemoveMenuItem;
using PetProjectCafe.Application.OrderFeatures.Commands.UpdateStatus;
using PetProjectCafe.Application.OrderFeatures.Queries.GetAllByPeriodDateTime;

namespace PetProjectCafe.API.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllByPeriodDateTime(
        [FromQuery] GetAllByFilteredRequest request,
        [FromServices] GetAllByFilteredHandler handler,
        CancellationToken cancellationToken)
    {
        var command = new GetAllByFilteredCommand(
            request.TimeIntervalFrom,
            request.TimeIntervalTo,
            request.Status);

        var result = await handler.Handle(command, cancellationToken);

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateOrderRequest request,
        [FromServices] CreateOrderHandler handler,
        CancellationToken cancellationToken)
    {
        var command = new CreateOrderCommand(request.Name, request.PaymentMethod, request.OrderItems);
        var result = await handler.Handle(command, cancellationToken);
        if (result.IsFailure)
            return BadRequest(result.Error);

        return CreatedAtAction(nameof(Create), result.Value);
    }

    [HttpPatch("{orderId:guid}")]
    public async Task<IActionResult> UpdateStatus(
        [FromRoute] Guid orderId,
        [FromBody] UpdateOrderStatusRequest request,
        [FromServices] UpdateOrderStatusHandler handler,
        CancellationToken cancellationToken)
    {
        var command = new UpdateOrderStatusCommand(orderId, request.Status);
        var result = await handler.Handle(command, cancellationToken);
        if (result.IsFailure)
            return BadRequest(result.Error);

        return Ok();
    }

    [HttpPost("{orderId:guid}/items")]
    public async Task<IActionResult> AddOrderItem(
        [FromRoute] Guid orderId,
        [FromBody] CreateOrderItemRequest request,
        [FromServices] CreateOrderItemHandler handler,
        CancellationToken cancellationToken)
    {
        var command = new CreateOrderItemCommand(orderId, request.MenuItemId, request.Quantity);
        var result = await handler.Handle(command, cancellationToken);
        if (result.IsFailure)
            return BadRequest(result.Error);

        return CreatedAtAction(nameof(AddOrderItem), result.Value);
    }

    [HttpDelete("{orderId:guid}/items/{orderItemId:guid}")]
    public async Task<IActionResult> RemoveOrderItem(
        [FromRoute] Guid orderId,
        [FromRoute] Guid orderItemId,
        [FromServices] RemoveOrderItemHandler handler,
        CancellationToken cancellationToken)
    {
        var command = new RemoveOrderItemCommand(orderId, orderItemId);
        var result = await handler.Handle(command, cancellationToken);
        if (result.IsFailure)
            return BadRequest(result.Error);

        return Ok();
    }
}