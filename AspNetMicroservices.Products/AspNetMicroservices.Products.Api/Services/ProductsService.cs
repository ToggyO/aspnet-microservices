using System.Threading.Tasks;

using AspNetMicroservices.Products.Business.Features.Products.Commands;
using AspNetMicroservices.Shared.Protos;
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
        /// Creates an instance of <see cref="ProductsService"/>.
        /// </summary>
        public ProductsService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override async Task<TestResponse> Test(Empty request, ServerCallContext context)
        {
            // return base.Test(request, context);
            // return Task.FromResult(new TestResponse { TestMessage = "KEK" });
            // var error = new ErrorResponse<TestResponse>
            // {
            //     Message = ErrorMessages.Global.Forbidden,
            //     HttpStatusCode = HttpStatusCode.Forbidden,
            //     Code = ErrorCodes.Global.Forbidden,
            // };
            // throw new ErrorResponseRpcException<TestResponse>(StatusCode.Internal, error);
            // var testModel = await _mediator.Send(new AddProduct.Command());
            return new TestResponse();
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
            return new ProductDto
            {
                Id = productModel.Id,
                Name = productModel.Name,
                Description = productModel.Description,
                Price = productModel.Price,
            };
        }
    }
}