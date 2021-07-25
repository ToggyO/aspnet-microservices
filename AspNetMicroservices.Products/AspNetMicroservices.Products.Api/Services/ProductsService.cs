using System.Collections.Generic;
using System.Threading.Tasks;

using AspNetMicroservices.Products.Business.Features.Products.Commands;
using AspNetMicroservices.Products.Business.Features.Products.Models;
using AspNetMicroservices.Products.Business.Features.Products.Queries;
using AspNetMicroservices.Products.Common.Extensions;
using AspNetMicroservices.Shared.Exceptions;
using AspNetMicroservices.Shared.Models.QueryFilter.Implementation;
using AspNetMicroservices.Shared.Models.Response;
using AspNetMicroservices.Shared.Protos;
using AspNetMicroservices.Shared.Protos.Common;

using AutoMapper;

using Google.Protobuf.WellKnownTypes;

using Grpc.Core;

using MediatR;

namespace AspNetMicroservices.Products.Api.Services
{
    /// <summary>
    /// Products service. Handles Grpc calls from different remote clients.
    /// </summary>
    public class ProductsService : Shared.Protos.ProductsService.ProductsServiceBase
    {
        /// <summary>
        /// Instance of <see cref="IMediator"/>.
        /// </summary>
        private readonly IMediator _mediator;

        /// <summary>
        /// Instance of <see cref="IMapper"/>.
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Creates an instance of <see cref="ProductsService"/>.
        /// </summary>
        public ProductsService(
	        IMediator mediator,
	        IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Method retrieves a list of products with pagination.
        /// </summary>
        /// <param name="request">Instance of <see cref="QueryFilterRequest"/>.</param>
        /// <param name="context">Instance of <see cref="ServerCallContext"/>.</param>
        /// <returns>List of products with pagination.</returns>
        public override async Task<ProductsListDto> GetProductsList(QueryFilterRequest request, ServerCallContext context)
        {
	        var filter = _mapper.Map<QueryFilterRequest, QueryFilterModel>(request);
			var pageModel = await _mediator.Send(new GetProductsList.Query(filter));
			var productsListDto = new ProductsListDto();
			productsListDto.Items.AddRange(
				_mapper.Map<IEnumerable<ProductModel>, IEnumerable<ProductDto>>(pageModel.Items));
			productsListDto.Pagination = pageModel.ToPaginationDto();
			return productsListDto;
        }

        /// <summary>
        /// Method retrieves product by identifier.
        /// </summary>
        /// <param name="request">Instance of <see cref="RetrieveSingleEntityRequest"/>.</param>
        /// <param name="context">Instance of <see cref="ServerCallContext"/>.</param>
        /// <returns>Requested product.</returns>
        /// <exception cref="ErrorResponseRpcException">Throws, if product with provided identifier not found.</exception>
        public override async Task<ProductDto> GetProductById(RetrieveSingleEntityRequest request, ServerCallContext context)
        {
	        var productModel = await _mediator.Send(new GetProduct.Query { Id = request.Id });
	        if (productModel is null)
		        throw new ErrorResponseRpcException(StatusCode.NotFound, new NotFoundErrorResponse());
	        return _mapper.Map<ProductDto>(productModel);
        }

        /// <summary>
        /// Method creates product.
        /// </summary>
        /// <param name="dto">Instance of <see cref="CreateProductDto"/>.</param>
        /// <param name="context">Instance of <see cref="ServerCallContext"/>.</param>
        /// <returns>Created product.</returns>
        public override async Task<ProductDto> CreateProduct(CreateProductDto dto, ServerCallContext context)
        {
	        var model = _mapper.Map<CreateProductDto, CreateProduct.Command>(dto);
	        var productModel = await _mediator.Send(model);
	        if (productModel is null)
	            throw new ErrorResponseRpcException(StatusCode.Internal, new InternalErrorResponse());
            return _mapper.Map<ProductModel, ProductDto>(productModel);
        }

        public override async Task<ProductDto> UpdateProduct(ProductDto dto, ServerCallContext context)
        {
	        var model = _mapper.Map<ProductDto, UpdateProduct.Command>(dto);
	        var productModel = await _mediator.Send(model);
	        if (productModel is null)
		        throw new ErrorResponseRpcException(StatusCode.NotFound, new NotFoundErrorResponse());
	        return _mapper.Map<ProductModel, ProductDto>(productModel);
        }

        public override async Task<Empty> RemoveProduct(RemoveSingleEntityRequest dto, ServerCallContext context)
        {
	        await _mediator.Send(new DeleteProduct.Command { Id = dto.Id });
	        return new Empty();
        }
    }
}