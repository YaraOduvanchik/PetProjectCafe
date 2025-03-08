using PetProjectCafe.Application.DTOs;
using PetProjectCafe.Application.OrderFeatures.Commands.Create;

namespace PetProjectCafe.API.Controllers.Requests.Orders;

public record CreateOrderRequest(
    string Name,
    string PaymentMethod,
    IEnumerable<OrderItemDto> OrderItems);