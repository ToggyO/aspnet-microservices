using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using AspNetMicroservices.Products.Common.Extensions;
using AspNetMicroservices.Products.DataLayer.Entities.Product;
using AspNetMicroservices.Products.DataLayer.Repositories.Products;
using AspNetMicroservices.Shared.Models.Pagination;
using AspNetMicroservices.Shared.Models.QueryFilter;
using AspNetMicroservices.Shared.Models.QueryFilter.Implementation;
using AspNetMicroservices.Shared.Protos;

using AutoMapper;

using MediatR;

namespace AspNetMicroservices.Products.Business.Features.Products.Queries
{
	/// <summary>
	/// Get products list query
	/// </summary>
	public class GetProductsList
	{
		/// <summary>
		/// Get products list query
		/// </summary>
		public sealed class Query : QueryFilterModel, IRequest<PaginationModel<ProductModel>>
		{
			public Query(IQueryFilter filter) : base(filter)
			{
			}
		}

		/// <summary>
		/// Handle <see cref="Query"/>.
		/// </summary>
		public sealed class Handler : IRequestHandler<Query, PaginationModel<ProductModel>>
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
			public async Task<PaginationModel<ProductModel>> Handle(Query request, CancellationToken cancellationToken)
			{
				var paginationModel = await _repository.GetList(request);
				return new PaginationModel<ProductModel>
				{
					Page = paginationModel.Page,
					PageSize = paginationModel.PageSize,
					Total = paginationModel.Total,
					Items = _mapper.Map<IEnumerable<ProductEntity>, IEnumerable<ProductModel>>(paginationModel.Items),
				};
			}
		}
	}
}