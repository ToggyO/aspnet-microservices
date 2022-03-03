using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AspNetMicroservices.Abstractions.Models.Response;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AspNetMicroservices.Auth.Api.Filters
{
	public class ValidationFilter : IAsyncActionFilter
	{
		public async Task OnActionExecutionAsync(ActionExecutingContext context,
			ActionExecutionDelegate next)
		{
			if (!context.ModelState.IsValid)
			{
				var errorsInModelState = context.ModelState
					.Where(x => x.Value.Errors.Count > 0)
					.ToDictionary(kvp => kvp.Key,
						kvp => kvp.Value.Errors.Select(x => x.ErrorMessage))
					.ToArray();

				var errors = new List<Error>();
			
				foreach (var error in errorsInModelState)
				{
					foreach (var subError in error.Value)
					{
						errors.Add(new Error
						{
							Code = "code",
							Message = subError,
							Field = error.Key,
						});
					}
				}

				context.Result = new ObjectResult(new BadParametersErrorResponse { Errors = errors });
				return;
			}

			await next();
		}
	}
}
