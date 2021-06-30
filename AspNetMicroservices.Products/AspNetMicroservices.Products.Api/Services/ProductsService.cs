using System.Threading.Tasks;
using AspNetMicroservices.Shared.Protos.ProductsProtos;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace AspNetMicroservices.Products.Api.Services
{
    public class ProductsService : Shared.Protos.ProductsProtos.ProductsService.ProductsServiceBase
    {
        public override Task<TestResponse> Test(Empty request, ServerCallContext context)
        {
            // return base.Test(request, context);
            // return Task.FromResult(new TestResponse { TestMessage = "KEK" });
            throw new RpcException(new Status(StatusCode.Internal, "Test"));
        }
    }
}