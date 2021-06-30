using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;

namespace AspNetMicroservices.Gateway.Api.Handlers.Products
{
    public interface IProductsHandler
    {
        Task<Empty> Test();
    }
}