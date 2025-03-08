using Microsoft.Extensions.DependencyInjection;
using PetProjectCafe.Application.Abstractions;
using PetProjectCafe.Infrastructure.Repositories;

namespace PetProjectCafe.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        return services
            .AddContexts()
            .AddRepositories();
    }

    private static IServiceCollection AddContexts(this IServiceCollection services)
    {
       return services.AddDbContext<ApplicationDbContext>();
    }
    
    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        return services.AddScoped<IMenuRepository, MenuRepository>();
    }
}