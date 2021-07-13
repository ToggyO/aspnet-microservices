using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AspNetMicroservices.Shared.Models.Response;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace AspNetMicroservices.Products.Business.Behaviours
{
    /// <summary>
    /// Provides validation pipeline behavior to surround the inner handler.
    /// Implementations add additional behavior and await the next delegate.
    /// </summary>
    /// <typeparam name="TRequest">MediatR request object.</typeparam>
    /// <typeparam name="TResponse">MediatR response object.</typeparam>
    public class ValidationBehaviours<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse> where TResponse : Response
    {
        /// <summary>
        /// List of validators.
        /// </summary>
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        /// <summary>
        /// Creates an instance of <see cref="ValidationBehaviours{TRequest,TResponse}"/>.
        /// </summary>
        /// <param name="validators">List of validators.</param>
        public ValidationBehaviours(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        /// <summary>
        /// Pipeline handler. Perform any additional validation behavior and await the <paramref name="next"/> delegate as necessary.
        /// </summary>
        /// <param name="request">Incoming request.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <param name="next">Awaitable delegate for the next action in the pipeline. Eventually this delegate represents the handler.</param>
        /// <returns>Awaitable task returning the <typeparamref name="TResponse"/>.</returns>
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
                IList<ValidationFailure> errors = results.SelectMany(r => r.Errors)
                    .Where(e => e != null)
                    .ToList();
            }

            return await next();
        }
    }
}