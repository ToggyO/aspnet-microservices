using System.Threading.Tasks;

using AspNetMicroservices.Gateway.Api.Extensions;
using AspNetMicroservices.Shared.Protos;
using AspNetMicroservices.Shared.Models.Response;
using AspNetMicroservices.Shared.Protos.Common;

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
		    => _client = client;

	    /// <inheritdoc cref="IProductsHandler.GetProductsList"/>
	    public async Task<Response<ProductsListDto>> GetProductsList(QueryFilterRequest filter)
		    => await _client.GetProductsListAsync(filter).HandleRpcCall();

	    /// <inheritdoc cref="IProductsHandler.GetProductById"/>
	    public async Task<Response<ProductDto>> GetProductById(int id)
		     => await _client.GetProductByIdAsync(new RetrieveSingleEntityRequest { Id = id }).HandleRpcCall();

	    /// <inheritdoc cref="IProductsHandler.CreateProduct"/>
        public async Task<Response<ProductDto>> CreateProduct(CreateProductDto dto) =>
	        await _client.CreateProductAsync(dto).HandleRpcCall();

	    /// <inheritdoc cref="IProductsHandler.UpdateProduct"/>
	    public async Task<Response<ProductDto>> UpdateProduct(int id, CreateProductDto dto)
		    => await _client.UpdateProductAsync(new ProductDto
		    {
			    Id = id,
			    Name = dto.Name,
			    Description = dto.Description,
			    Price = dto.Price,
		    }).HandleRpcCall();

	    /// <inheritdoc cref="IProductsHandler.DeleteProduct"/>
	    public async Task<Response> DeleteProduct(int id)
	    {
		    await _client.RemoveProductAsync(new RemoveSingleEntityRequest { Id = id });
		    return new Response();
	    }
    }
}