using System;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetMicroservices.Shared.Extensions
{
    /// <summary>
    /// Provides an extension methods for <see cref="IServiceCollection"/> class.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds a service of the type specified in <typeparamref name="TService"/> with an
        /// implementation type specified in <typeparamref name="TImplementation"/> to the
        /// specified <see cref="IServiceCollection"/> with lifetime of <see cref="ServiceLifetime"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
        /// <param name="serviceLifetime">The lifetime of the added service <see cref="ServiceLifetime"/></param>
        /// <typeparam name="TService">The type of the service to add.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation to use.</typeparam>
        /// <exception cref="ArgumentOutOfRangeException">Instance of <see cref="ArgumentOutOfRangeException"/></exception>
        public static void Add<TService, TImplementation>(this IServiceCollection services,
            ServiceLifetime serviceLifetime) where TService : class where TImplementation : class, TService
        {
            switch (serviceLifetime)
            {
                case ServiceLifetime.Transient:
                    services.AddTransient<TService, TImplementation>();
                    break;
                case ServiceLifetime.Scoped:
                    services.AddScoped<TService, TImplementation>();
                    break;
                case ServiceLifetime.Singleton:
                    services.AddSingleton<TService, TImplementation>();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(serviceLifetime), serviceLifetime, null);
            }
        }

        /// <summary>
        /// Adds a service with an implementation type
        /// specified in <typeparamref name="TService"/> to the
        /// specified <see cref="IServiceCollection"/> with lifetime of <see cref="ServiceLifetime"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
        /// <param name="serviceLifetime">The lifetime of the added service <see cref="ServiceLifetime"/></param>
        /// <typeparam name="TService">The implementation of the service to add.</typeparam>
        public static void Add<TService>(this IServiceCollection services,
            ServiceLifetime serviceLifetime) where TService : class
        {
            services.Add<TService, TService>(serviceLifetime);
        }
    }
}