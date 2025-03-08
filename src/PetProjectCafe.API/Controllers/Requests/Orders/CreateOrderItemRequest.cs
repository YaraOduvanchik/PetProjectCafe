namespace PetProjectCafe.API.Controllers.Requests.Orders;

public record CreateOrderItemRequest(Guid MenuItemId, int Quantity);