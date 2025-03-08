namespace PetProjectCafe.Application.OrderFeatures.Commands.RemoveMenuItem;

public record RemoveOrderItemCommand(Guid OrderId, Guid OrderItemId);