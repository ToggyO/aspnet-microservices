using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using AspNetMicroservices.Gateway.Api.Handlers.Products;
using AspNetMicroservices.Shared.Models.Response;
using AspNetMicroservices.Shared.Protos;
using AspNetMicroservices.Shared.Protos.Common;

namespace AspNetMicroservices.Gateway.Api.Controllers.V1
{
	/// <summary>
	/// Products controller.
	/// </summary>
    [ApiController]
    [Route("products")]
    public class ProductsController : ControllerBase, IProductsHandler
    {
	    /// <summary>
	    /// Instance of <see cref="IProductsHandler"/>.
	    /// </summary>
        private readonly IProductsHandler _handler;

	    /// <summary>
	    /// Creates an instance of <see cref="ProductsController"/>.
	    /// </summary>
	    /// <param name="handler">Instance of <see cref="IProductsHandler"/>.</param>
        public ProductsController(IProductsHandler handler)
        {
            _handler = handler;
        }

	    /// <inheritdoc cref="IProductsHandler.GetProductsList"/>
	    [HttpGet]
	    public async Task<Response<ProductsListDto>> GetProductsList([FromQuery] QueryFilterRequest filter)
		    => await _handler.GetProductsList(filter);

	    /// <inheritdoc cref="IProductsHandler.CreateProduct"/>
        [HttpPost]
        public async Task<Response<ProductDto>> CreateProduct([FromBody] CreateUpdateProductDTO dto) =>
	        await _handler.CreateProduct(dto);

    }
}