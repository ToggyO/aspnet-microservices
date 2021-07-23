using System.Threading;
using System.Threading.Tasks;

using AspNetMicroservices.Products.Business.Features.Products.Models;
using AspNetMicroservices.Products.DataLayer.Entities.Product;
using AspNetMicroservices.Products.DataLayer.Repositories.Products;
using AspNetMicroservices.Shared.Errors;

using AutoMapper;

using FluentValidation;

using MediatR;

namespace AspNetMicroservices.Products.Business.Features.Products.Commands
{
	/// <summary>
	/// Create product command.
	/// </summary>
    public static class CreateProduct
    {
	    /// <summary>
	    /// Create product command.
	    /// </summary>
        public sealed class Command : IRequest<ProductModel>
        {
	        /// <summary>
	        /// Gets or sets the product name.
	        /// </summary>
            public string Name { get; set; }

	        /// <summary>
	        /// Gets or sets the product description.
	        /// </summary>
            public string Description { get; set; }

	        /// <summary>
	        /// Gets or sets the product price.
	        /// </summary>
            public long Price { get; set; }
        }

	    /// <summary>
	    /// Handle <see cref="Command"/>.
	    /// </summary>
        public sealed class Handler : IRequestHandler<Command, ProductModel>
	    {
		    /// <summary>
		    /// Instance of <see cref="IProductsRepository"/>.
		    /// </summary>
		    private readonly IProductsRepository _repository;

            /// <summary>
            /// Instance of <see cref="IMapper"/>.
            /// </summary>
            private readonly IMapper _mapper;

            /// <summary>
            /// Creates an instance of <see cref="Handler"/>.
            /// </summary>
            /// <param name="repository">Instance of <see cref="IProductsRepository"/>.</param>
            /// <param name="mapper">Instance of <see cref="IMapper"/>.</param>
            public Handler(
	            IProductsRepository repository,
	            IMapper mapper)
            {
	            _repository = repository;
                _mapper = mapper;
            }

            /// <inheritdoc cref="IRequestHandler{TRequest,TResponse}"/>
            public async Task<ProductModel> Handle(Command cmd, CancellationToken ct)
            {
                var entity = new ProductEntity
                {
                    Name = cmd.Name,
                    Description = cmd.Description,
                    Price = cmd.Price,
                };
                return _mapper.Map<ProductEntity, ProductModel>(await _repository.Create(entity));
            }
        }

	    #region Validator

	    /// <summary>
	    /// Validator for <see cref="Command"/>.
	    /// </summary>
	    public sealed class Validator : AbstractValidator<Command>
	    {
		    /// <summary>
		    /// Creates an instance of <see cref="Validator"/>.
		    /// </summary>
		    public Validator()
		    {
			    RuleFor(x => x.Name).NotEmpty().WithErrorCode(ErrorCodes.Validation.FieldNotEmpty);
			    RuleFor(x => x.Price).NotEmpty().WithErrorCode(ErrorCodes.Validation.FieldNotEmpty);
		    }
	    }

	    #endregion
    }
}