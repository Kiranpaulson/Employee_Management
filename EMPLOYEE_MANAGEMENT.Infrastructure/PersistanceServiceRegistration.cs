using EMPLOYEE_MANAGEMENT.Application.Absractions.Repositories;
using EMPLOYEE_MANAGEMENT.Application.logging;
using EMPLOYEE_MANAGEMENT.Infrastructure.Logging;
using EMPLOYEE_MANAGEMENT.Infrastructure.Persistance;
using EMPLOYEE_MANAGEMENT.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EMPLOYEE_MANAGEMENT.Infrastructure
{
    /// <summary>
    /// Extension class used to register persistence-layer services,
    /// including database context and repository dependencies.
    /// </summary>
    public static class PersistanceServiceRegistration
    {
        /// <summary>
        /// Adds database context, repositories, and logging services 
        /// to the application's dependency injection container.
        /// </summary>
        /// <param name="services">The service collection used for dependency injection.</param>
        /// <param name="configuration">Application configuration for accessing connection strings.</param>
        /// <returns>The updated service collection.</returns>
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Register PostgreSQL DbContext
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            // Register Generic and Specific Repositories
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();

            // Register Logging Adapter
            services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));

            return services;
        }
    }
}
