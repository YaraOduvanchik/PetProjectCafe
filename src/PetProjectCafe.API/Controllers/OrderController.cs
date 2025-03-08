using Microsoft.AspNetCore.Mvc;
using PetProjectCafe.API.Controllers.Requests.Orders;
using PetProjectCafe.Application.OrderFeatures.Commands.Create;
using PetProjectCafe.Application.OrderFeatures.Commands.UpdateStatus;

namespace PetProjectCafe.API.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
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
}