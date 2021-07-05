using System.Net;
using System.Threading.Tasks;
using AspNetMicroservices.Gateway.Api.Extensions;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

using AspNetMicroservices.Shared.Protos;
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
            var response = await _client
                .TestAsync(new Empty())
                .EnsureSuccess();
            return response;
        }
    }
}