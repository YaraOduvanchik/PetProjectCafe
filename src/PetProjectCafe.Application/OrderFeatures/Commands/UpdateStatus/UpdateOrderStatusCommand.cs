namespace PetProjectCafe.Application.OrderFeatures.Commands.UpdateStatus;

public record UpdateOrderStatusCommand(Guid OrderId, string Status);