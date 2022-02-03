using System.Reflection;

using AspNetMicroservices.Auth.Application.Common.MediatR.Behaviours;
using AspNetMicroservices.Auth.Application.Mappings;

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
		/// <param name="serviceLifetime">Service lifetime enumeration.</param>
		public static void AddApplicationLayer(this IServiceCollection services,
			ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
		{
			var assembly = Assembly.GetExecutingAssembly();

			services.AddMediatR(assembly);
			services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

			services.AddMapsterMapper(serviceLifetime);
			services.AddFluentValidation(fv
				=> fv.RegisterValidatorsFromAssembly(assembly));
		}
	}
}