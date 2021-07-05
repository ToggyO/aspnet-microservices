using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using AspNetMicroservices.Gateway.Api.Handlers.Products;
using AspNetMicroservices.Shared.Models.Response;
using AspNetMicroservices.Shared.Protos;

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

        [HttpGet]
        public async Task<Response<TestResponse>> Test() => await _handler.Test();
    }
}