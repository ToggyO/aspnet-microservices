using Microsoft.Extensions.DependencyInjection;

using AspNetMicroservices.Gateway.Api.Filters;
using AspNetMicroservices.Gateway.Api.Handlers.Products;
using AspNetMicroservices.Gateway.Api.Handlers.Products.Implementation;
using AspNetMicroservices.Shared.Extensions;

namespace AspNetMicroservices.Gateway.Api
{
    public static class DependencyInjectionModule
    {
        public static void Load(IServiceCollection services,
            ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
        {
	        Common.DependencyInjectionModule.Load(services);

            services.Add<IProductsHandler, ProductsHandler>(serviceLifetime);
        }

        public static void LoadRpcDependencies(IServiceCollection services,
            ServiceLifetime serviceLifetime = ServiceLifetime.Singleton)
        {
            services.Add<RpcErrorInterceptor>(serviceLifetime);
        }
    }
}