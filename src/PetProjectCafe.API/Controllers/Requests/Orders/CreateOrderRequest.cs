using PetProjectCafe.Application.DTOs;

namespace PetProjectCafe.API.Controllers.Requests.Orders;

public record CreateOrderRequest(
    string Name,
    string PaymentMethod,
    IEnumerable<OrderItemDto> OrderItems);