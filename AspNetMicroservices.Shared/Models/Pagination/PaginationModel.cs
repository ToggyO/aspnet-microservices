using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AspNetMicroservices.Shared.Constants.Common;
using AspNetMicroservices.Shared.Contracts;

namespace AspNetMicroservices.Shared.Models.Pagination
{
	/// <summary>
	/// Represents common pagination model. pagination model with data collection of type <see cref="T"/>.
	/// </summary>
	/// <typeparam name="TItem">Type of data collection.</typeparam>
    public class PaginationModel<TItem> : IPaginatedQuery
    {
	    /// <inheritdoc cref="IPaginatedQuery.Page"/>.
	    public int Page { get; set; } = PaginationDefaults.DefaultPageNumber;

	    /// <inheritdoc cref="IPaginatedQuery.PageSize"/>.
	    public int PageSize { get; set; } = PaginationDefaults.DefaultPageSize;

	    /// <summary>
	    /// Total count of elements.
	    /// </summary>
        public int Total { get; set; }

	    /// <summary>
	    /// List of items.
	    /// </summary>
        public IEnumerable<TItem> Items { get; set; }
    }
}