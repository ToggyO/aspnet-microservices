using System.Threading.Tasks;

using AspNetMicroservices.Products.DataLayer.DataBase.AppDataConnection;
using AspNetMicroservices.Products.DataLayer.Entities.Product;

using LinqToDB;

namespace AspNetMicroservices.Products.DataLayer.Repositories.Products.Implementation
{
	// TODO: add summary.
	public partial class ProductsRepository
	{
		private readonly AppDataConnection _connection;

		public ProductsRepository(AppDataConnection connection)
		{
			_connection = connection;
		}

		public async Task<ProductEntity> Create(ProductEntity entity)
		{
			entity.Id = await _connection.InsertWithInt32IdentityAsync(entity);
			return entity;
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