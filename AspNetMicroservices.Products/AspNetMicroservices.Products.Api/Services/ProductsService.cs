using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using AspNetMicroservices.Shared.Errors;
using AspNetMicroservices.Shared.Exceptions;
using AspNetMicroservices.Shared.Models.Response;
using AspNetMicroservices.Shared.Protos;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace AspNetMicroservices.Products.Api.Services
{
    public class ProductsService : Shared.Protos.ProductsService.ProductsServiceBase
    {
        public override Task<TestResponse> Test(Empty request, ServerCallContext context)
        {
            // return base.Test(request, context);
            // return Task.FromResult(new TestResponse { TestMessage = "KEK" });
            var error = new ErrorResponse<TestResponse>
            {
                Message = ErrorMessages.Global.Forbidden,
                HttpStatusCode = HttpStatusCode.Forbidden,
                Code = ErrorCodes.Global.Forbidden,
            };
            throw new ErrorResponseRpcException<TestResponse>(StatusCode.Internal, error);
        }
    }
}