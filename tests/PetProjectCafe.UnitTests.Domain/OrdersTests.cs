using FluentAssertions;
using PetProjectCafe.Domain.Orders;
using PetProjectCafe.Domain.Orders.ValueObjects;
using PetProjectCafe.Domain.ValueObjects;
using PetProjectCafe.Domain.ValueObjects.Ids;

namespace PetProjectCafe.UnitTests.Domain;

public class OrdersTests
{
    [Fact]
    public void Create_WithValidData_ReturnsSuccess()
    {
        // Arrange
        var id = OrderId.NewId();
        var clientName = Name.Create("Test").Value;
        var paymentMethod = PaymentMethod.Create("Cash").Value;

        // Act
        var order = new Order(id, clientName, paymentMethod);

        // Assert
        order.Should().NotBeNull();
        order.ClientName.Should().Be(clientName);
        order.PaymentMethod.Should().Be(paymentMethod);
        order.DateAndTime.Should().NotBe(default);
        order.Status.Should().Be(OrderStatus.AtWork);
    }

    [Theory]
    [InlineData("AtWork", "Completed", true, "Completed")]
    [InlineData("AtWork", "Cancelled", true, "Cancelled")]
    [InlineData("Completed", "AtWork", true, "AtWork")]
    [InlineData("Completed", "Cancelled", false, "Completed")]
    [InlineData("Cancelled", "AtWork", true, "AtWork")]
    [InlineData("Cancelled", "Completed", false, "Cancelled")]
    public void UpdateStatus_WithValidData_ReturnsExpectedResult(
        string fromStatus,
        string toStatus,
        bool expectedSuccess,
        string expectedStatus)
    {
        // Arrange
        var id = OrderId.NewId();
        var clientName = Name.Create("Test").Value;
        var paymentMethod = PaymentMethod.Create("Cash").Value;
        var order = new Order(id, clientName, paymentMethod);
        var from = OrderStatus.Create(fromStatus).Value;
        var to = OrderStatus.Create(toStatus).Value;
        order.UpdateStatus(from);

        // Act
        var result = order.UpdateStatus(to);

        // Assert
        result.IsSuccess.Should().Be(expectedSuccess);
        order.Status.Should().Be(OrderStatus.Create(expectedStatus).Value);
    }

    [Fact]
    public void AddOrderItem_WithValidData_ReturnsSuccess()
    {
        // Arrange
        var id = OrderId.NewId();
        var clientName = Name.Create("Test").Value;
        var paymentMethod = PaymentMethod.Create("Cash").Value;
        var order = new Order(id, clientName, paymentMethod);

        // Act
        order.AddOrderItem(OrderItem.Create(MenuItemId.NewId(), 10).Value);

        // Assert
        order.OrderItems.Should().HaveCount(1);
    }

    [Fact]
    public void RemoveOrderItem_ExistingItem_ReturnsSuccess()
    {
        // Arrange
        var id = OrderId.NewId();
        var clientName = Name.Create("Test").Value;
        var paymentMethod = PaymentMethod.Create("Cash").Value;
        var order = new Order(id, clientName, paymentMethod);
        order.AddOrderItem(OrderItem.Create(MenuItemId.NewId(), 10).Value);

        var orderItemId = order.OrderItems.First().Id;

        // Act
        order.RemoveOrderItem(orderItemId);

        // Assert
        order.OrderItems.Should().HaveCount(0);
    }

    [Fact]
    public void RemoveOrderItem_NonExistingItem_ReturnsFailure()
    {
        // Arrange
        var id = OrderId.NewId();
        var clientName = Name.Create("Test").Value;
        var paymentMethod = PaymentMethod.Create("Cash").Value;
        var order = new Order(id, clientName, paymentMethod);

        // Act
        var result = order.RemoveOrderItem(OrderItemId.NewId());

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be("Order item not found!");
    }
}