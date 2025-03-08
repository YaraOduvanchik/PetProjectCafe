using PetProjectCafe.Application.DTOs;

namespace PetProjectCafe.Application.OrderFeatures.Commands.Create;

public record CreateOrderCommand(
    string Name,
    string PaymentMethod,
    IEnumerable<OrderItemDto> OrderItems);