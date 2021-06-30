using System.Threading.Tasks;
using AspNetMicroservices.Gateway.Api.Handlers.Products;
using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Mvc;

namespace AspNetMicroservices.Gateway.Api.Controllers.V1
{
    [ApiController]
    [Route("products")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsHandler _handler;

        public ProductsController(IProductsHandler handler)
        {
            _handler = handler;
        }

        [HttpGet]
        public async Task<Empty> Test() => await _handler.Test();
    }
}