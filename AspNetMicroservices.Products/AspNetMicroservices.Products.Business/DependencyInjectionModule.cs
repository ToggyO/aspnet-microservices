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
        public static void Load(IServiceCollection services,
            ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
        {
        }
    }
}