using System.Threading.Tasks;

using AspNetMicroservices.Products.Business.Features.Products;
using AspNetMicroservices.Products.Business.Features.Products.Commands;
using AspNetMicroservices.Shared.Protos;
using AspNetMicroservices.Shared.Protos.Common;

using AutoMapper;

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

        public override Task GetProductsList(QueryFilterModel request, IServerStreamWriter<ProductDto> responseStream, ServerCallContext context)
        {
	        return base.GetProductsList(request, responseStream, context);
        }

        /// <summary>
        /// Method creates product.
        /// </summary>
        /// <param name="dto">Instance of <see cref="CreateProductDTO"/>.</param>
        /// <param name="context">Instance of <see cref="ServerCallContext"/>.</param>
        /// <returns>Created product.</returns>
        public override async Task<ProductDto> CreateProduct(CreateProductDTO dto, ServerCallContext context)
        {
            var cmd = new CreateProduct.Command
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
            };
            var productModel = await _mediator.Send(cmd);
            return _mapper.Map<ProductModel, ProductDto>(productModel);
        }
    }
}