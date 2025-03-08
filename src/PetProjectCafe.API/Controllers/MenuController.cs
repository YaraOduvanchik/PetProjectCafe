using Microsoft.AspNetCore.Mvc;
using PetProjectCafe.API.Controllers.Requests;
using PetProjectCafe.Application.MenuFeatures.Commands.Create;
using PetProjectCafe.Application.MenuFeatures.Commands.CreateMenuItem;
using PetProjectCafe.Application.MenuFeatures.Commands.RemoveMenuItem;
using PetProjectCafe.Application.MenuFeatures.Commands.UpdateMenuItem;

namespace PetProjectCafe.API.Controllers;

[ApiController]
[Route("[controller]")]
public class MenuController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateMenuRequest request,
        [FromServices] CreateMenuHandler handler,
        CancellationToken cancellationToken)
    {
        var result = await handler.Handle(new CreateMenuCommand(request.Name), cancellationToken);
        if (result.IsFailure)
            return BadRequest(result.Error);

        return CreatedAtAction(nameof(Create), result.Value);
    }

    [HttpPost("{menuId:guid}/items")]
    public async Task<IActionResult> CreateMenuItem(
        [FromRoute] Guid menuId,
        [FromBody] CreateMenuItemRequest request,
        [FromServices] CreateMenuItemHandler handler,
        CancellationToken cancellationToken)
    {
        var command = new CreateMenuItemCommand(menuId, request.Name, request.Price);

        var result = await handler.Handle(command, cancellationToken);
        if (result.IsFailure)
            return BadRequest(result.Error);

        return CreatedAtAction(nameof(CreateMenuItem), result.Value);
    }

    [HttpPatch("{menuId:guid}/items/{menuItemId:guid}")]
    public async Task<IActionResult> UpdateMenuItem(
        [FromRoute] Guid menuId,
        [FromRoute] Guid menuItemId,
        [FromBody] UpdateMenuItemRequest request,
        [FromServices] UpdateMenuItemHandler handler,
        CancellationToken cancellationToken)
    {
        var command = new UpdateMenuItemCommand(menuId, menuItemId, request.Name, request.Price);

        var result = await handler.Handle(command, cancellationToken);
        if (result.IsFailure)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }
    
    [HttpDelete("{menuId:guid}/items/{menuItemId:guid}")] 
    public async Task<IActionResult> RemoveMenuItem(
        [FromRoute] Guid menuId,
        [FromRoute] Guid menuItemId,
        [FromServices] RemoveMenuItemHandler handler,
        CancellationToken cancellationToken)
    {
        var result = await handler.Handle(new RemoveMenuItemCommand(menuId, menuItemId), cancellationToken);
        if (result.IsFailure)
            return BadRequest(result.Error);

        return Ok();
    }
}