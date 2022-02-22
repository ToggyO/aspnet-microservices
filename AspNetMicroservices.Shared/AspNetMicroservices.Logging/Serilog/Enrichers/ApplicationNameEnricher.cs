using AspNetMicroservices.Common.Utils;

using Serilog.Core;
using Serilog.Events;

namespace AspNetMicroservices.Logging.Serilog.Enrichers
{
	public class ApplicationNameEnricher : ILogEventEnricher
	{
		private const string PropName = "Application";

		public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
		{
			var solutionName = Project.GetCurrentSolutionName();
			var eventType = propertyFactory.CreateProperty(PropName, solutionName);
			logEvent.AddPropertyIfAbsent(eventType);
		}
	}
}