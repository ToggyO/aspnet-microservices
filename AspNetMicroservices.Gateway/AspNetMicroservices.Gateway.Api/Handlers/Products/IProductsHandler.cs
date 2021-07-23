using System.Threading.Tasks;

using AspNetMicroservices.Shared.Models.Response;
using AspNetMicroservices.Shared.Protos;
using AspNetMicroservices.Shared.Protos.Common;

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
        /// <param name="dto">Instance of <see cref="CreateUpdateProductDTO"/>.</param>
        /// <returns>Created product</returns>
        Task<Response<ProductDto>> CreateProduct(CreateUpdateProductDTO dto);
    }
}