using AspNetMicroservices.Common.Utils;

using Serilog.Core;
using Serilog.Events;

namespace AspNetMicroservices.Logging.Serilog.Enrichers
{
	public class ApplicationNameEnricher : ILogEventEnricher
	{
		private const string PropName = "Application";

		private readonly string _currentSolutionName = Project.GetCurrentSolutionName();

		public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
		{
			var eventType = propertyFactory.CreateProperty(PropName, _currentSolutionName);
			logEvent.AddPropertyIfAbsent(eventType);
		}
	}
}