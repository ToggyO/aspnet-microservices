using System;

using Serilog;
using Serilog.Configuration;

namespace AspNetMicroservices.Logging.Serilog.Enrichers.Extensions
{
	public static class EnricherExtensions
	{
		public static LoggerConfiguration WithApplicationName(
			this LoggerEnrichmentConfiguration enrich)
		{
			if (enrich is null)
				throw new ArgumentNullException(nameof(enrich));

			return enrich.With<ApplicationNameEnricher>();
		}
	}
}