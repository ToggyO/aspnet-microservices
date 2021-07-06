using Microsoft.Extensions.DependencyInjection;

namespace AspNetMicroservices.Products.Business
{
    public static class DependencyInjectionModule
    {
        public static void Load(IServiceCollection services,
            ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
        {
        }
    }
}