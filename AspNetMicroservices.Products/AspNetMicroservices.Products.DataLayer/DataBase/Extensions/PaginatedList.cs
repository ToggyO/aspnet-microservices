using System.Linq;
using System.Threading.Tasks;

using AspNetMicroservices.Shared.Contracts;
using AspNetMicroservices.Shared.Models.Pagination;

using LinqToDB;

namespace AspNetMicroservices.Products.DataLayer.DataBase.Extensions
{
	/// <summary>
	/// Pagination model factory.
	/// </summary>
	public static class PaginatedList
	{
		/// <summary>
		/// Default page number to get.
		/// </summary>
		private static readonly int _defaultPage = 1;

		/// <summary>
		/// Default number of items on each page.
		/// </summary>
		private static readonly int _defaultPageSize = 10;

		/// <summary>
		/// Creates paginated list.
		/// </summary>
		/// <typeparam name="TSource">Item type.</typeparam>
		/// <param name="source">Queryable source.</param>
		/// <param name="query">Page query to get.</param>
		/// <returns>Paginated list.</returns>
		public static PaginationModel<TSource> ToPaginatedList<TSource>(this IQueryable<TSource> source,
			IPaginatedQuery query)
			=> Create(source, query);

		/// <summary>
		/// Creates paginated list.
		/// </summary>
		/// <typeparam name="TSource">Item type.</typeparam>
		/// <param name="source">Queryable source.</param>
		/// <param name="query">Page query to get.</param>
		/// <returns>Paginated list.</returns>
		public static async Task<PaginationModel<TSource>> ToPaginatedListAsync<TSource>(this IQueryable<TSource> source,
			IPaginatedQuery query) => await CreateAsync(source, query);

		/// <summary>
		/// Converts source of type <see cref="IQueryable{TSource}"/> to type of <see cref="IList{TSource}"/>.
		/// </summary>
		/// <param name="source">Source of type <see cref="IQueryable{TSource}"/>.</param>
		/// <param name="query">Instance of <see cref="IPaginatedQuery"/>.</param>
		/// <typeparam name="TSource">Type of source.</typeparam>
		/// <returns>List of type of source.</returns>
		public static PaginationModel<TSource> Create<TSource>(this IQueryable<TSource> source, IPaginatedQuery query)
		{
			int skipCount = GetPageDbIndexNumber(query);
			return new PaginationModel<TSource>
			{
				Page = query.Page,
				PageSize = query.PageSize < 0 ? _defaultPageSize : query.PageSize,
				Total = source.Count(),
				Items = source.Skip(skipCount).Take(query.PageSize).ToList(),
			};
		}

		/// <summary>
		/// Creates paginated list.
		/// </summary>
		/// <typeparam name="TSource">List item type.</typeparam>
		/// <param name="source">Queryable source.</param>
		/// <param name="query">Page query to get.</param>
		/// <returns>Paginated list.</returns>
		public static async Task<PaginationModel<TSource>> CreateAsync<TSource>(IQueryable<TSource> source, IPaginatedQuery query)
		{
			int skipCount = GetPageDbIndexNumber(query);
			return new PaginationModel<TSource>
			{
				Page = query.Page,
				PageSize = query.PageSize < 0 ? _defaultPageSize : query.PageSize,
				Total = await source.CountAsync(),
				Items = await source.Skip(skipCount).Take(query.PageSize).ToListAsync(),
			};
		}

		/// <summary>
		/// Check if page index is valid.
		/// </summary>
		/// <param name="query">Page query to get.</param>
		/// <returns>Page index.</returns>
		private static int GetPageDbIndexNumber(IPaginatedQuery query)
		{
			int queryPage = (query.Page - 1) * query.PageSize;
			return queryPage < 0 ? _defaultPage : queryPage;
		}
	}
}