using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VSR.DataTracker.Infrastructure.Abstractions.Repositories;
using VSR.DataTracker.Infrastructure.Repositories;

namespace VSR.DataTracker.Infrastructure
{
    public static class DIRegister
    {
        public static IServiceCollection AddDataBase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<VesselDataTrackerDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("VesselDb")));

            services.AddScoped<IVesselRepository, VesselRepository>();


            return services;
        }

        public static void MigrateDataBase(this IServiceProvider services)
        {
            using (var serviceScope = services.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<VesselDataTrackerDbContext>();
                context.Database.Migrate();
            }
        }
    }
}