using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PetProjectCafe.Application.Abstractions;
using PetProjectCafe.Domain.Menus;
using PetProjectCafe.Domain.ValueObjects.Ids;

namespace PetProjectCafe.Infrastructure.Repositories;

public class MenuRepository : IMenuRepository
{
    private readonly ApplicationDbContext _context;

    public MenuRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<Menu>> GetById(MenuId id, CancellationToken cancellationToken)
    {
        var menu = await _context.Menus
            .Include(m => m.MenuItems)
            .SingleOrDefaultAsync(m => m.Id == id, cancellationToken);

        if (menu is null)
            return Result.Failure<Menu>("Menu not found!");

        return menu;
    }

    public async Task Create(Menu menu, CancellationToken cancellationToken)
    {
        await _context.Menus.AddAsync(menu, cancellationToken);
    }

    public void Update(Menu menu, CancellationToken cancellationToken)
    {
        _context.Menus.Update(menu);
    }

    public async Task<UnitResult<string>> Delete(MenuId id, CancellationToken cancellationToken)
    {
        var menu = await _context.Menus.SingleOrDefaultAsync(m => m.Id == id, cancellationToken);
        if (menu is null)
            return UnitResult.Failure<string>("Menu not found!");

        _context.Menus.Remove(menu);

        await _context.SaveChangesAsync(cancellationToken);

        return UnitResult.Success<string>();
    }

    public Task SaveChanges(CancellationToken cancellationToken)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }
}