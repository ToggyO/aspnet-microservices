using AspNetMicroservices.Products.DataLayer.Repositories.Products;
using AspNetMicroservices.Products.DataLayer.Repositories.Products.Implementation;
using AspNetMicroservices.Shared.Extensions;

using Microsoft.Extensions.DependencyInjection;

namespace AspNetMicroservices.Products.DataLayer
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
	        services.Add<IProductsRepository, ProductsRepository>(serviceLifetime);
        }
    }
}