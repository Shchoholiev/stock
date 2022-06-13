using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Stock.Application.IRepositories;
using Stock.Application.IServices;
using Stock.Infrastructure.EF;
using Stock.Infrastructure.Repositories;
using Stock.Infrastructure.Services;

namespace Stock.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            var connectionString = @"server=(LocalDb)\MSSQLLocalDB;database=Stock;integrated security=True;
                    MultipleActiveResultSets=True;App=EntityFramework;";

            services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(connectionString)
            );

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IItemsService, ItemsService>();

            return services;
        }
    }
}
