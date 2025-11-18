using EMPLOYEE_MANAGEMENT.Application.Common.Behaviour;
using EMPLOYEE_MANAGEMENT.Application.Validators;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace EMPLOYEE_MANAGEMENT.Application
{
    /// <summary>
    /// Registers all application-layer services including MediatR, validators,
    /// AutoMapper profiles, and pipeline behaviors.
    /// </summary>
    public static class ApplicationServiceRegistration
    {
        /// <summary>
        /// Adds all required application-level dependencies to the service collection.
        /// This includes:
        /// <list type="bullet">
        /// <item><description>AutoMapper registration</description></item>
        /// <item><description>MediatR handlers and requests</description></item>
        /// <item><description>FluentValidation validators</description></item>
        /// <item><description>Pipeline validation behavior</description></item>
        /// </list>
        /// </summary>
        /// <param name="services">The service collection to register dependencies into.</param>
        /// <returns>The updated service collection.</returns>
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            /// <summary>
            /// Registers AutoMapper and loads mapping profiles from the Application assembly.
            /// </summary>
            services.AddAutoMapper(typeof(ApplicationServiceRegistration).Assembly);

            /// <summary>
            /// Registers all MediatR handlers (Commands, Queries, Notifications) 
            /// from the Application assembly.
            /// </summary>
            services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(typeof(ApplicationServiceRegistration).Assembly));

            /// <summary>
            /// Registers all FluentValidation validators automatically from the assembly.
            /// </summary>
            services.AddValidatorsFromAssembly(typeof(CreateEmployeeCommandValidator).Assembly);

            /// <summary>
            /// Adds a MediatR pipeline behavior that automatically runs validation
            /// before executing any handler.
            /// </summary>
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            return services;
        }
    }
}
