using System.Threading.Tasks;
using AspNetMicroservices.Products.DataLayer.Entities.Product;

namespace AspNetMicroservices.Products.DataLayer.Repositories.Products.Implementation
{
    // TODO: check
    public class ProductsRepository : IProductsRepository
    {
        public Task<ProductEntity> GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<ProductEntity> Create(ProductEntity entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<ProductEntity> Update(ProductEntity entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<ProductEntity> Delete(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}