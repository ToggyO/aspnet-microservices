﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AspNetMicroservices.Shared.Models.Response;

using FluentValidation;
using FluentValidation.Results;

using MediatR;
using MediatR.Pipeline;

namespace AspNetMicroservices.Auth.Application.Common.Behaviours
{
	public class ValidationBehaviour<TRequest, TResponse>
		: IPipelineBehavior<TRequest, TResponse>
	{
		private readonly IEnumerable<IValidator<TRequest>> _validators;

		public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
		{
			_validators = validators;
		}

		public async Task<TResponse> Handle(
			TRequest request,
			CancellationToken cancellationToken,
			RequestHandlerDelegate<TResponse> next)
		{
			if (_validators.Any())
			{
				var context = new ValidationContext<TRequest>(request);

				ValidationResult[] results = await Task.WhenAll(
					_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
				IList<ValidationFailure> validationErrors = results.SelectMany(r => r.Errors)
					.Where(e => e != null)
					.ToList();

				if (validationErrors.Any())
				{
					// var error = new BadParametersErrorResponse(validationErrors.Select(GetError).ToArray());
					throw new ValidationException(validationErrors);
				}
			}

			return await next();
		}

		protected Error GetError(ValidationFailure error)
		{
			return new Error
			{
				Code = error.ErrorCode,
				Message = error.ErrorMessage,
				Field = error.PropertyName,
			};
		}
	}

	public class ValidationExceptionHandler<TRequest, TResponse, TException>
		: RequestExceptionHandler<TRequest, TResponse, TException> where TException : Exception
	{
		protected override void Handle(TRequest request, TException exception, RequestExceptionHandlerState<TResponse> state)
		{
			Console.WriteLine("ValidationExceptionHandler");
		}
	}
}