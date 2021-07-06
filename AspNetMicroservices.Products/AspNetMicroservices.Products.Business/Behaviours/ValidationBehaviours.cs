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
    public class ValidationBehaviours<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse> where TResponse : Response
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehaviours(IEnumerable<IValidator<TRequest>> validators)
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
                IList<ValidationFailure> errors = results.SelectMany(r => r.Errors)
                    .Where(e => e != null)
                    .ToList();
            }

            return await next();
        }
    }
}