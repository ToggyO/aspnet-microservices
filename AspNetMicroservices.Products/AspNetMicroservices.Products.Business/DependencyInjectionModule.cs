using AspNetMicroservices.Products.Common.Behaviours;
using AspNetMicroservices.Products.Common.Settings;

using FluentValidation;

using MediatR;

using Microsoft.Extensions.DependencyInjection;

namespace AspNetMicroservices.Products.Business
{
    /// <summary>
    /// Adds additional services to IoC container.
    /// </summary>
    public static class DependencyInjectionModule
    {
        /// <summary>
        /// Method loads a set of services to IoC container.
        /// </summary>
        /// <param name="services">Instance of <see cref="IServiceCollection"/>.</param>
        /// <param name="serviceLifetime">Service lifetime.</param>
        public static void AddBusinessLayer(this IServiceCollection services,
            ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
        {
	        services.AddMediatR(typeof(Business.DependencyInjectionModule));
	        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
	        services.AddValidatorsFromAssembly(typeof(Business.DependencyInjectionModule).Assembly);
	        ValidatorConfigurationOverload.Override();
        }
    }
}