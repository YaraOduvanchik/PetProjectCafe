using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PetProjectCafe.Application.Abstractions;
using PetProjectCafe.Domain.Orders;
using PetProjectCafe.Domain.ValueObjects.Ids;

namespace PetProjectCafe.Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly ApplicationDbContext _context;

    public OrderRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<Order>> GetById(OrderId id, CancellationToken cancellationToken)
    {
        var order = await _context.Orders
            .Include(o => o.OrderItems)
            .SingleOrDefaultAsync(o => o.Id == id, cancellationToken: cancellationToken);

        if (order is null)
            return Result.Failure<Order>("Order not found!");

        return order;
    }

    public async Task Create(Order order, CancellationToken cancellationToken)
    {
        await _context.Orders.AddAsync(order, cancellationToken);
    }

    public void Update(Order order, CancellationToken cancellationToken)
    {
        _context.Orders.Update(order);
    }

    public async Task<UnitResult<string>> Delete(OrderId id, CancellationToken cancellationToken)
    {
        var order = await _context.Orders.SingleOrDefaultAsync(m => m.Id == id, cancellationToken);
        if (order is null)
            return UnitResult.Failure<string>("Menu not found!");

        _context.Orders.Remove(order);

        await _context.SaveChangesAsync(cancellationToken);

        return UnitResult.Success<string>();
    }

    public Task SaveChanges(CancellationToken cancellationToken)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }
}