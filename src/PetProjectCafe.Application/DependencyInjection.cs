using Microsoft.Extensions.DependencyInjection;
using PetProjectCafe.Application.MenuFeatures.Commands.Create;
using PetProjectCafe.Application.MenuFeatures.Commands.CreateMenuItem;
using PetProjectCafe.Application.MenuFeatures.Commands.RemoveMenuItem;
using PetProjectCafe.Application.MenuFeatures.Commands.UpdateMenuItem;

namespace PetProjectCafe.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services.AddHandlers();
    }

    private static IServiceCollection AddHandlers(this IServiceCollection services)
    {
        services.AddScoped<CreateMenuHandler>();
        services.AddScoped<CreateMenuItemHandler>();
        services.AddScoped<UpdateMenuItemHandler>();
        services.AddScoped<RemoveMenuItemHandler>();

        return services;
    }
}