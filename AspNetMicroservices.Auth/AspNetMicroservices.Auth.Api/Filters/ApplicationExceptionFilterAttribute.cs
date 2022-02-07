using System;

using AspNetMicroservices.Shared.Exceptions;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AspNetMicroservices.Auth.Api.Filters
{
	/// <summary>
	/// Represents application exception filter.
	/// </summary>
    public class ApplicationExceptionFilterAttribute : ExceptionFilterAttribute
    {
	    /// <inheritdoc cref="IExceptionFilter.OnException"/>.
	    /// Catches and handles exceptions of given types.
        public override void OnException(ExceptionContext context)
        {
            switch (context.Exception)
            {
	            case ApplicationValidationException e:
	                context.Result = new BadRequestObjectResult(e.GetErrorContent());
	                return;
            }
        }
    }
}