using System.Threading.Tasks;

using AspNetMicroservices.Abstractions.Models.Response;
using AspNetMicroservices.Grpc.Protos.Common;
using AspNetMicroservices.Grpc.Protos.Products;

namespace AspNetMicroservices.Gateway.Api.Handlers.Products
{
	/// <summary>
	/// Represents products handler.
	/// </summary>
    public interface IProductsHandler
	{
		/// <summary>
		/// Get a list of products with pagination.
		/// </summary>
		/// <param name="filter">Instance of <see cref="QueryFilterRequest"/>.</param>
		/// <returns>List of products with pagination.</returns>
		Task<Response<ProductsListDto>> GetProductsList(QueryFilterRequest filter);

		/// <summary>
		/// Get product by identifier.
		/// </summary>
		/// <param name="id">Product identifier.</param>
		/// <returns>Requested product.</returns>
		Task<Response<ProductDto>> GetProductById(int id);

        /// <summary>
        /// Create product.
        /// </summary>
        /// <param name="dto">Instance of <see cref="CreateProductDto"/>.</param>
        /// <returns>Created product.</returns>
        Task<Response<ProductDto>> CreateProduct(CreateProductDto dto);

        /// <summary>
        /// Update product.
        /// </summary>
        /// <param name="id">Product id.</param>
        /// <param name="dto">Instance of <see cref="CreateProductDto"/>.</param>
        /// <returns>Updated product.</returns>
        Task<Response<ProductDto>> UpdateProduct(int id, CreateProductDto dto);

        /// <summary>
        /// Delete product.
        /// </summary>
        /// <param name="id">Product id.</param>
        /// <returns>Instance od <see cref="Response"/>.</returns>
        Task<Response> DeleteProduct(int id);
    }
}