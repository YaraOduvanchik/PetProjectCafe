namespace PetProjectCafe.Application.OrderFeatures.Commands.CreateOrderItem;

public record CreateOrderItemCommand(Guid OrderId, Guid MenuItemId, int Quantity);