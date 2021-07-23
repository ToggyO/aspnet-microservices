using System;
using System.Linq;
using System.Threading.Tasks;

using AspNetMicroservices.Products.DataLayer.Common;
using AspNetMicroservices.Products.DataLayer.DataBase.Extensions;
using AspNetMicroservices.Products.DataLayer.Entities.Product;
using AspNetMicroservices.Shared.Models.Pagination;
using AspNetMicroservices.Shared.Models.QueryFilter.Implementation;

using LinqToDB;

namespace AspNetMicroservices.Products.DataLayer.Repositories.Products.Implementation
{
	/// <inheritdoc cref="IProductsRepository"/>
	public partial class ProductsRepository : IProductsRepository
    {
	    /// <inheritdoc cref="IProductsRepository.GetList"/>
	    public async Task<PaginationModel<ProductEntity>> GetList(QueryFilterModel filter)
	    {
		    var products = from p in _connection.Products select p;

		    if (!string.IsNullOrEmpty(filter.Search))
		    {
			    products = from p in products
					where p.Name.Contains(filter.Search, StringComparison.OrdinalIgnoreCase)
				    select p;
		    }

		    return await products.ToPaginatedListAsync(filter);
	    }


	    /// <inheritdoc cref="IProductsRepository.GetById"/>
	    public Task<ProductEntity> GetById(int id) => _connection.Products.GetById(id);
    }
}