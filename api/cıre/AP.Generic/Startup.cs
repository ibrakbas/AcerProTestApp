using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using AP.Generic.Services;

namespace AP.Generic;

public static class Startup
{
    /// <summary> 
    /// Bu method <see cref="ServiceLifetime"/> servis ömrünü ve <see cref="{TDbContext}"/> veritabanı bağlamını alır.
    /// Ayrıca bu metod kendi veritabanı bağlamı için kolay depo kütüphanesini uygular
    /// </summary>


    public static IServiceCollection ApplyEasyRepository<TDbContext>(this IServiceCollection services, ServiceLifetime serviceLifetime = ServiceLifetime.Transient) where TDbContext : DbContext
    {
        services.Add(new ServiceDescriptor(
            typeof(IRepository),
            serviceProvider =>
            {
                var dbContext = ActivatorUtilities.CreateInstance<TDbContext>(serviceProvider);
                return new Repository(dbContext);
            },
            serviceLifetime));

        services.Add(new ServiceDescriptor(
            typeof(IUnitOfWork),
            serviceProvider =>
            {
                var repository = serviceProvider.GetService<IRepository>();
                return new UnitOfWork(repository);
            },
            serviceLifetime));
        return services;
    }
}
