using CSharpFunctionalExtensions;
using PetProjectCafe.Domain.Menus;
using PetProjectCafe.Domain.ValueObjects.Ids;

namespace PetProjectCafe.Application.Abstractions;

public interface IMenuRepository
{
    Task<Result<Menu>> GetById(MenuId id, CancellationToken cancellationToken);
    Task Create(Menu menu, CancellationToken cancellationToken);
    void Update(Menu menu, CancellationToken cancellationToken);
    Task<UnitResult<string>> Delete(MenuId id, CancellationToken cancellationToken);
    Task SaveChanges(CancellationToken cancellationToken);
}