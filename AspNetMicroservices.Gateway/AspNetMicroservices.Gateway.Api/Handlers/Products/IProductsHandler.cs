using System.Threading.Tasks;

using AspNetMicroservices.Shared.Models.Response;
using AspNetMicroservices.Shared.Protos;

namespace AspNetMicroservices.Gateway.Api.Handlers.Products
{
    public interface IProductsHandler
    {
        Task<Response<TestResponse>> Test();

        /// <summary>
        /// Create product.
        /// </summary>
        /// <param name="dto">Instance of <see cref="CreateProductDTO"/>.</param>
        /// <returns>Created product</returns>
        Task<Response<ProductDto>> CreateProduct(CreateProductDTO dto);
    }
}