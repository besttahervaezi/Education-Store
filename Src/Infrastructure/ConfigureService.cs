using Application.Contracts;
using Infrastructure.Persistance;
using Infrastructure.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class ConfigureService
{
    public static IServiceCollection AddInfraStructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(option =>
        {
            option.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });
      //  services.AddScoped(typeof(GenericRepository<>), typeof(IGenericRepository<>));
        services.AddScoped<IUnitOfWork, UnitofWork>();
        return services;
    }
}