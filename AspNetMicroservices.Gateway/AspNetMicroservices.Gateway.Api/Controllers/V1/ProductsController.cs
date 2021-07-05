using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using AspNetMicroservices.Gateway.Api.Handlers.Products;
using AspNetMicroservices.Shared.Models.Response;
using AspNetMicroservices.Shared.Protos;

namespace AspNetMicroservices.Gateway.Api.Controllers.V1
{
    [ApiController]
    [Route("products")]
    public class ProductsController : ControllerBase, IProductsHandler
    {
        private readonly IProductsHandler _handler;

        public ProductsController(IProductsHandler handler)
        {
            _handler = handler;
        }

        [HttpGet]
        public async Task<Response<TestResponse>> Test() => await _handler.Test();
    }
}