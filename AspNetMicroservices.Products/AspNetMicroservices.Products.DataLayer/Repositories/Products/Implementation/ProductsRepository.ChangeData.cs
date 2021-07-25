using System.Threading.Tasks;

using AspNetMicroservices.Products.DataLayer.DataBase.AppDataConnection;
using AspNetMicroservices.Products.DataLayer.Entities.Product;
using AspNetMicroservices.Products.DataLayer.Transactions;

using LinqToDB;

namespace AspNetMicroservices.Products.DataLayer.Repositories.Products.Implementation
{
	/// <inheritdoc cref="IProductsRepository"/>
	public partial class ProductsRepository : TransactionBuilder
	{
		/// <summary>
		/// Instance of <see cref="AppDataConnection"/>.
		/// </summary>
		private readonly AppDataConnection _connection;

		/// <summary>
		/// Initialize new instance of <see cref="ProductsRepository"/>.
		/// </summary>
		/// <param name="connection">Database connection.</param>
		public ProductsRepository(AppDataConnection connection) : base(connection)
		{
			_connection = connection;
		}

		/// <inheritdoc cref="IProductsRepository.Create"/>
		public async Task<ProductEntity> Create(ProductEntity entity)
		{
			entity.Id = await _connection.InsertWithInt32IdentityAsync(entity);
			return entity;
		}

		/// <inheritdoc cref="IProductsRepository.Update"/>
		public async Task<ProductEntity> Update(ProductEntity entity)
		{
			await _connection.UpdateAsync(entity);
			return entity;
		}

		/// <inheritdoc cref="IProductsRepository.Delete"/>
		public async Task<ProductEntity> Delete(int id)
		{
			await _connection.Products.DeleteAsync(x => x.Id == id);
			return await GetById(id);
		}
	}
}