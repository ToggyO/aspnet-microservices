using Microsoft.Extensions.DependencyInjection;

using AspNetMicroservices.Gateway.Api.Filters;
using AspNetMicroservices.Gateway.Api.Handlers.Products;
using AspNetMicroservices.Gateway.Api.Handlers.Products.Implementation;
using AspNetMicroservices.Gateway.Api.Interceptors;
using AspNetMicroservices.Shared.Extensions;

namespace AspNetMicroservices.Gateway.Api
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
	        Common.DependencyInjectionModule.Load(services);

            services.Add<IProductsHandler, ProductsHandler>(serviceLifetime);
        }

	    /// <summary>
	    /// Method loads a set of Rpc services to DI container.
	    /// </summary>
	    /// <param name="services">Instance of <see cref="IServiceCollection"/>.</param>
	    /// <param name="serviceLifetime">Service lifetime.</param>
        public static void LoadRpcDependencies(IServiceCollection services,
		    // TODO: check service lifetime
		    ServiceLifetime serviceLifetime = ServiceLifetime.Singleton)
        {
	        services.AddHttpContextAccessor();
            services.Add<RpcErrorInterceptor>(serviceLifetime);
            services.Add<AuthHeadersInterceptor>(ServiceLifetime.Transient);
        }
    }
}