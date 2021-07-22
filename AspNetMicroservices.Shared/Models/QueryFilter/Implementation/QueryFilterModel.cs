using AspNetMicroservices.Shared.Contracts;

namespace AspNetMicroservices.Shared.Models.QueryFilter.Implementation
{
	/// <summary>
	/// Represents common filter for data querying.
	/// </summary>
	public class QueryFilterModel : IQueryFilter
	{
		/// <inheritdoc cref="IQueryFilter.Search"/>.
		public string Search { get; set; }

		/// <inheritdoc cref="IQueryFilter.Page"/>.
		public int Page { get; set; } = 1;

		/// <inheritdoc cref="IQueryFilter.PageSize"/>.
		public int PageSize { get; set; } = 10;

		/// <summary>
		/// Creates an instance of <see cref="QueryFilterModel"/>.
		/// </summary>
		public QueryFilterModel()
		{
		}

		/// <summary>
		/// Creates an instance of <see cref="QueryFilterModel"/>.
		/// </summary>
		/// <param name="filter">Instance of <see cref="IQueryFilter"/>,</param>
		public QueryFilterModel(IQueryFilter filter)
		{
			// TODO: CHECK DEFAULT VALUES
			Search = filter.Search;
			Page = filter.Page;
			PageSize = filter.PageSize;
		}
	}
}