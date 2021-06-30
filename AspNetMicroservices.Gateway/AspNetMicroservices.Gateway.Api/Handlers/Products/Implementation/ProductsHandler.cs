using System.Threading.Tasks;
using AspNetMicroservices.Shared.Protos.ProductsProtos;
using Google.Protobuf.WellKnownTypes;

namespace AspNetMicroservices.Gateway.Api.Handlers.Products.Implementation
{
    public class ProductsHandler : IProductsHandler
    {
        private readonly ProductsService.ProductsServiceClient _client;

        public ProductsHandler(ProductsService.ProductsServiceClient client)
        {
            _client = client;
        }

        public async Task<Empty> Test()
        {
            
            var response = await _client.TestAsync(new Empty());
            return response;
        }
    }
}