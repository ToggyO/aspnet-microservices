using System.Net;
using System.Threading.Tasks;
using AspNetMicroservices.Products.Business.Features.Products.Commands;
using AspNetMicroservices.Shared.Errors;
using AspNetMicroservices.Shared.Exceptions;
using AspNetMicroservices.Shared.Models.Response;
using AspNetMicroservices.Shared.Protos;
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
            var testModel = await _mediator.Send(new AddProduct.Command());
            return new TestResponse {TestMessage = testModel.Name};
        }
    }
}