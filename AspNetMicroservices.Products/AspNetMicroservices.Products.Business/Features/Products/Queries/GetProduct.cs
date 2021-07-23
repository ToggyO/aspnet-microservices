using System.Threading;
using System.Threading.Tasks;

using AspNetMicroservices.Products.Business.Features.Products.Models;
using AspNetMicroservices.Products.DataLayer.Entities.Product;
using AspNetMicroservices.Products.DataLayer.Repositories.Products;
using AspNetMicroservices.Shared.Contracts;

using AutoMapper;

using MediatR;

namespace AspNetMicroservices.Products.Business.Features.Products.Queries
{
	/// <summary>
	/// Get product identifier id.
	/// </summary>
	public class GetProduct
	{
		/// <summary>
		/// Get product by identifier query.
		/// </summary>
		public sealed class Query : IHaveIdentifier, IRequest<ProductModel>
		{
			/// <inheritdoc cref="IHaveIdentifier"/>
			public int Id { get; set; }
		}

		/// <summary>
		/// Handle <see cref="Query"/>.
		/// </summary>
		public sealed class Handler : IRequestHandler<Query, ProductModel>
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
			public async Task<ProductModel> Handle(Query query, CancellationToken cancellationToken = default)
			{
				var entity = await _repository.GetById(query.Id);
				if (entity is null)
					return null;
				return _mapper.Map<ProductEntity, ProductModel>(entity);
			}
		}
	}
}