using System;
using System.Threading;
using System.Threading.Tasks;

using AspNetMicroservices.Products.DataLayer.Repositories.Products;
using AspNetMicroservices.Shared.Contracts;

using MediatR;

namespace AspNetMicroservices.Products.Business.Features.Products.Commands
{
	/// <summary>
	/// Delete product.
	/// </summary>
	public class DeleteProduct
	{
		/// <summary>
		/// Delete product command.
		/// </summary>
		public sealed class Command : IHaveIdentifier, IRequest
		{
			/// <inheritdoc cref="IHaveIdentifier"/>.
			public int Id { get; set; }
		}

		/// <summary>
		/// Handle <see cref="Command"/>.
		/// </summary>
		public sealed class Handler : IRequestHandler<Command>
		{
			/// <summary>
			/// Instance of <see cref="IProductsRepository"/>.
			/// </summary>
			private readonly IProductsRepository _repository;

			/// <summary>
			/// Creates an instance of <see cref="Handler"/>.
			/// </summary>
			/// <param name="repository">Instance of <see cref="IProductsRepository"/>.</param>
			public Handler(IProductsRepository repository) => _repository = repository;

			/// <inheritdoc cref="IRequestHandler{TRequest,TResponse}"/>
			public async Task<Unit> Handle(Command cmd, CancellationToken cancellationToken = default)
			{
				await _repository.Delete(cmd.Id);
				return Unit.Value;
			}
		}
	}
}