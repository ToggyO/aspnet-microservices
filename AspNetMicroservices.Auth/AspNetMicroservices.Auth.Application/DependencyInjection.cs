using System.Reflection;

using FluentValidation.AspNetCore;

using MediatR;

using Microsoft.Extensions.DependencyInjection;

namespace AspNetMicroservices.Auth.Application
{
	public static class DependencyInjection
	{
		/// <summary>
		/// Add application layer module to an application services.
		/// </summary>
		/// <param name="services">Instance of <see cref="IServiceCollection"/>.</param>
		public static void AddApplicationLayer(this IServiceCollection services)
		{
			var assembly = Assembly.GetExecutingAssembly();
			services.AddMediatR(assembly);
			services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssembly(assembly));
		}
	}
}