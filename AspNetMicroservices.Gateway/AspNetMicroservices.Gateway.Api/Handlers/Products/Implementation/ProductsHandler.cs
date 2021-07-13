using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;

using AspNetMicroservices.Gateway.Api.Extensions;
using AspNetMicroservices.Shared.Protos;
using AspNetMicroservices.Shared.Models.Response;

namespace AspNetMicroservices.Gateway.Api.Handlers.Products.Implementation
{
	/// <summary>
	/// Products handler.
	/// </summary>
    public class ProductsHandler : IProductsHandler
    {
	    /// <summary>
	    /// Instance of <see cref="ProductsService.ProductsServiceClient"/>.
	    /// </summary>
        private readonly ProductsService.ProductsServiceClient _client;

	    /// <summary>
	    /// Creates an instance of <see cref="ProductsHandler"/>.
	    /// </summary>
	    /// <param name="client">Instance of <see cref="ProductsService.ProductsServiceClient"/>.</param>
        public ProductsHandler(ProductsService.ProductsServiceClient client)
        {
            _client = client;
        }

        public async Task<Response<TestResponse>> Test() =>
	         await _client.TestAsync(new Empty()).EnsureRpcCallSuccess();

        /// <inheritdoc cref="IProductsHandler"/>
        public async Task<Response<ProductDto>> CreateProduct(CreateProductDTO dto) =>
	        await _client.CreateProductAsync(dto).EnsureRpcCallSuccess();
    }
}