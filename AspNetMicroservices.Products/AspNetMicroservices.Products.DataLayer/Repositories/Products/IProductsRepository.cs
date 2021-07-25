using System.Threading.Tasks;

using AspNetMicroservices.Products.DataLayer.Entities.Product;
using AspNetMicroservices.Shared.Contracts;
using AspNetMicroservices.Shared.Models.Pagination;
using AspNetMicroservices.Shared.Models.QueryFilter.Implementation;

using LinqToDB.Data;

namespace AspNetMicroservices.Products.DataLayer.Repositories.Products
{
	/// <summary>
	/// Represents products entity repository.
	/// </summary>
    public interface IProductsRepository : IBaseRepository<ProductEntity>, ITransactional<DataConnectionTransaction>
    {
	    /// <summary>
	    /// Retrieves a collection of <see cref="ProductEntity"/>.
	    /// </summary>
	    /// <param name="filter">Query filter data transfer object.</param>
	    /// <returns>A collection of <see cref="ProductEntity"/>.</returns>
	    Task<PaginationModel<ProductEntity>> GetList(QueryFilterModel filter);
    }
}