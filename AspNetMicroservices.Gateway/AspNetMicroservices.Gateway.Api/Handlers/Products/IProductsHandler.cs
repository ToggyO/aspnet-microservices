using System.Threading.Tasks;

using AspNetMicroservices.Shared.Models.Response;
using AspNetMicroservices.Shared.Protos.ProductsProtos;

namespace AspNetMicroservices.Gateway.Api.Handlers.Products
{
    public interface IProductsHandler
    {
        Task<Response<TestResponse>> Test();
    }
}