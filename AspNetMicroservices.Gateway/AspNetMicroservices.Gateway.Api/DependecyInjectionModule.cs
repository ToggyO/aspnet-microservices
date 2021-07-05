using Microsoft.Extensions.DependencyInjection;

using AspNetMicroservices.Gateway.Api.Filters;
using AspNetMicroservices.Gateway.Api.Handlers.Products;
using AspNetMicroservices.Gateway.Api.Handlers.Products.Implementation;
using AspNetMicroservices.Shared.Extensions;

namespace AspNetMicroservices.Gateway.Api
{
	/// <summary>
	/// Add to DI container additional services.
	/// </summary>
    public static class DependencyInjectionModule
    {
	    /// <summary>
	    /// Method loads a set of services to DI container.
	    /// </summary>
	    /// <param name="services">Instance of <see cref="IServiceCollection"/>.</param>
	    /// <param name="serviceLifetime">Service lifetime.</param>
        public static void Load(IServiceCollection services,
            ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
        {
	        Common.DependencyInjectionModule.Load(services);

            services.Add<IProductsHandler, ProductsHandler>(serviceLifetime);
        }

	    /// <summary>
	    /// Method loads a set of Rpc services to DI container.
	    /// </summary>
	    /// <param name="services">Instance of <see cref="IServiceCollection"/>.</param>
	    /// <param name="serviceLifetime">Service lifetime.</param>
        public static void LoadRpcDependencies(IServiceCollection services,
            ServiceLifetime serviceLifetime = ServiceLifetime.Singleton)
        {
            services.Add<RpcErrorInterceptor>(serviceLifetime);
        }
    }
}