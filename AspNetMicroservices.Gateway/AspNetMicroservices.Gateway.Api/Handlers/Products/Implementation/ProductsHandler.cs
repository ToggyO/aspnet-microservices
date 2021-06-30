using System.Net;
using System.Threading.Tasks;
using AspNetMicroservices.Gateway.Api.Extensions;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

using AspNetMicroservices.Shared.Protos.ProductsProtos;
using AspNetMicroservices.Shared.Models.Response;

namespace AspNetMicroservices.Gateway.Api.Handlers.Products.Implementation
{
    public class ProductsHandler : IProductsHandler
    {
        private readonly ProductsService.ProductsServiceClient _client;

        public ProductsHandler(ProductsService.ProductsServiceClient client)
        {
            _client = client;
        }

        public async Task<Response<TestResponse>> Test()
        {
            try
            {
                var response = await _client.TestAsync(new Empty());
                return new Response<TestResponse>
                {
                    Data = response,
                };
            }
            catch (RpcException e)
            {
                return new ErrorResponse<TestResponse>
                {
                    HttpStatusCode = e.StatusCode.ToHttpStatusCode(),
                    Message = e.Status.Detail,
                };
            }
        }

        
    }
}