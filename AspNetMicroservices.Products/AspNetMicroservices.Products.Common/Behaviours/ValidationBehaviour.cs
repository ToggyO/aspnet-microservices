using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AspNetMicroservices.Shared.Exceptions;
using AspNetMicroservices.Shared.Models.Response;
using FluentValidation;
using FluentValidation.Results;

using Grpc.Core;

using MediatR;

namespace AspNetMicroservices.Products.Common.Behaviours
{
    /// <inheritdoc cref="IPipelineBehavior{TRequest,TResponse}"/>
	/// <summary>
	/// Represents object validation from incoming request with <see cref="FluentValidation"/> package.
	/// </summary>
	/// <typeparam name="TRequest">Request type.</typeparam>
	/// <typeparam name="TResponse">Response type.</typeparam>
    public class ValidationBehaviour<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        /// <summary>
		/// Initialize of <see cref="ValidationBehaviour{TRequest,TResponse}"/>.
		/// </summary>
		/// <param name="validators">
		/// Set of validators <see cref="IValidator{TRequest}"/> of a given type.
		/// </param>
        public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        /// <inheritdoc cref="IPipelineBehavior{TRequest,TResponse}.Handle"/>.
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
	                var error = new BadParametersErrorResponse(validationErrors.Select(GetError).ToArray());
	                throw new ErrorResponseRpcException(StatusCode.InvalidArgument, error);
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
}