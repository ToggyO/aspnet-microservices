using System.Threading.Tasks;

using AspNetMicroservices.Abstractions.Contracts;
using AspNetMicroservices.Abstractions.Models.Pagination;
using AspNetMicroservices.Abstractions.Models.QueryFilter.Implementation;
using AspNetMicroservices.Products.DataLayer.Entities.Product;

using LinqToDB.Data;

namespace AspNetMicroservices.Products.DataLayer.Repositories.Products
{
	/// <summary>
	/// Represents products entity repository.
	/// </summary>
    public interface IProductsRepository : IBaseRepository<ProductEntity>, IAsyncTransactional<DataConnectionTransaction>
    {
	    /// <summary>
	    /// Retrieves a collection of <see cref="ProductEntity"/>.
	    /// </summary>
	    /// <param name="filter">Query filter data transfer object.</param>
	    /// <returns>A collection of <see cref="ProductEntity"/>.</returns>
	    Task<PaginationModel<ProductEntity>> GetList(QueryFilterModel filter);
    }
}