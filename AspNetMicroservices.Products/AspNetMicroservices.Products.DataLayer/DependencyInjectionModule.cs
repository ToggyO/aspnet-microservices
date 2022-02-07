using System.Reflection;

using AspNetMicroservices.Products.DataLayer.DataBase.AppDataConnection;
using AspNetMicroservices.Products.DataLayer.Repositories.Products;
using AspNetMicroservices.Products.DataLayer.Repositories.Products.Implementation;
using AspNetMicroservices.Shared.Extensions;

using FluentMigrator.Runner;

using LinqToDB.AspNet;
using LinqToDB.Configuration;

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
        /// <param name="connectionsString">Database connection string.</param>
        /// <param name="serviceLifetime">Service lifetime.</param>
        public static void AddDataLayer(this IServiceCollection services,
	        string connectionsString,
            ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
        {
	        services.AddLinqToDbContext<AppDataConnection>((provider, options) =>
	        {
		        options.UsePostgreSQL(connectionsString);
	        });

	        services.AddFluentMigratorCore()
		        .ConfigureRunner(cfg => cfg
			        .AddPostgres()
			        .WithGlobalConnectionString(connectionsString)
			        .ScanIn(Assembly.GetAssembly(typeof(AppDataConnection))).For.All()
		        )
		        .AddLogging(cfg => cfg.AddFluentMigratorConsole());

	        services.Add<IProductsRepository, ProductsRepository>(serviceLifetime);
        }
    }
}