using CSharpFunctionalExtensions;
using PetProjectCafe.Domain.Orders;
using PetProjectCafe.Domain.ValueObjects.Ids;

namespace PetProjectCafe.Application.Abstractions;

public interface IOrderRepository
{
    Task<IReadOnlyCollection<Order>> GetByPeriodDateTimeAndStatus(
        DateTime dateTimeFrom,
        DateTime dateTimeTo,
        string status,
        CancellationToken cancellationToken);

    Task<Result<Order>> GetById(OrderId id, CancellationToken cancellationToken);
    Task Create(Order order, CancellationToken cancellationToken);
    void Update(Order order, CancellationToken cancellationToken);
    Task<UnitResult<string>> Delete(OrderId id, CancellationToken cancellationToken);
    Task SaveChanges(CancellationToken cancellationToken);
}