namespace PetProjectCafe.Application.DTOs;

public record OrderDto(
    Guid OrderId,
    string ClientName,
    DateTime DateAndTime,
    string PaymentMethod,
    string Status,
    IEnumerable<OrderItemDto> OrderItems);