using System;

using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using AspNetMicroservices.Shared.Models.Response;

namespace AspNetMicroservices.Gateway.Api.Filters
{
	/// <summary>
	/// Takes HttpStatusCode from an instance of <see cref="Response"/> and
	/// sets to an instance of HttpResponse as StatusCode field.
	/// </summary>
	public class StatusCodeFilter : IResultFilter
	{
		/// <summary>
		/// Instance of <see cref="ILogger{TCategoryName}"/>.
		/// </summary>
		private readonly ILogger<StatusCodeFilter> _logger;

		/// <summary>
		/// Creates an instance of <see cref="StatusCodeFilter"/>.
		/// </summary>
		/// <param name="logger">Instance of <see cref="ILogger{TCategoryName}"/>.</param>
		public StatusCodeFilter(ILogger<StatusCodeFilter> logger)
		{
			_logger = logger;
		}

		/// <inheritdoc cref="IResultFilter"/>.
		public void OnResultExecuting(ResultExecutingContext context)
		{
			try
			{
				if (context.Result is ObjectResult objectResult)
				{
					if (objectResult.Value is Response response)
					{
						context.HttpContext.Response.StatusCode = (int)response.HttpStatusCode;
					}
				}
			}
			catch (Exception e)
			{
				_logger.LogError(e, e.Message);
			}
		}

		/// <inheritdoc cref="IResultFilter"/>.
		public void OnResultExecuted(ResultExecutedContext context)
		{
		}
	}
}