using System;

using AspNetMicroservices.Logging.Serilog.Enrichers.Extensions;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Serilog;

namespace AspNetMicroservices.Logging.Serilog
{
	/// <summary>
	/// Serilog extensions.
	/// </summary>
	public static class SerilogExtensions
	{
		/// <summary>
		/// Configure serilog logging for application domain.
		/// </summary>
		/// <param name="hostBuilder">Application host builder.</param>
		/// <returns></returns>
		public static IHostBuilder UseSerilogLogging(this IHostBuilder hostBuilder)
		{
			hostBuilder.UseSerilog();
			return hostBuilder;
		}

		/// <summary>
		/// Configure Serilog with instance of <see cref="IConfiguration"/>
		/// and adds an application services.
		/// </summary>
		/// <param name="services">Application services collection.</param>
		/// <param name="configuration">Application configuration.</param>
		/// <param name="enrichWithSolutionName">Application configuration.</param>
		/// <returns></returns>
		public static IServiceCollection AddConfiguredSerilog(this IServiceCollection services,
			IConfiguration configuration, bool enrichWithSolutionName = true)
		{
			try
			{
				var loggerConfiguration = new LoggerConfiguration()
					.ReadFrom.Configuration(configuration);

				if (enrichWithSolutionName)
					loggerConfiguration.Enrich.WithApplicationName();

				Log.Logger = loggerConfiguration.CreateLogger();
				services.AddSingleton(Log.Logger);

				return services;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
				throw;
			}
		}

		/// <summary>
		/// Dispose Serilog logger on application shutdown.
		/// </summary>
		/// <param name="lifetime">Application host lifetime instance.</param>
		/// <returns></returns>
		public static IHostApplicationLifetime UseSerilogCloseAndFlush(
			this IHostApplicationLifetime lifetime)
		{
			lifetime.ApplicationStopping.Register(Log.CloseAndFlush);
			return lifetime;
		}
	}
}
