﻿using Microsoft.AspNetCore.Mvc;
using PetProjectCafe.API.Controllers.Requests.Orders;
using PetProjectCafe.Application.OrderFeatures.Commands.Create;
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
}