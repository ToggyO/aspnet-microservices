using System;
using System.Linq;
using System.Threading.Tasks;

using AspNetMicroservices.Products.DataLayer.Common;
using AspNetMicroservices.Products.DataLayer.DataBase.Extensions;
using AspNetMicroservices.Products.DataLayer.Entities.Product;
using AspNetMicroservices.Shared.Extensions;
using AspNetMicroservices.Shared.Models.Pagination;
using AspNetMicroservices.Shared.Models.QueryFilter.Implementation;

namespace AspNetMicroservices.Products.DataLayer.Repositories.Products.Implementation
{
	/// <inheritdoc cref="IProductsRepository"/>
	public partial class ProductsRepository : IProductsRepository
    {
	    /// <inheritdoc cref="IProductsRepository.GetList"/>
	    public async Task<PaginationModel<ProductEntity>> GetList(QueryFilterModel filter)
	    {
		    var products = from p in Connection.Products select p;

		    if (!string.IsNullOrEmpty(filter.Search))
			    products = from p in products
					where p.Name.Contains(filter.Search, StringComparison.OrdinalIgnoreCase)
				    select p;

		    if (!string.IsNullOrEmpty(filter.OrderBy))
			    products = products.TrySort(filter.OrderBy, filter.IsDesc);

		    return await products.ToPaginatedListAsync(filter);
	    }


	    /// <inheritdoc cref="IProductsRepository.GetById"/>
	    public Task<ProductEntity> GetById(int id) => Connection.Products.GetById(id);
    }
}