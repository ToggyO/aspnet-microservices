using System.Linq;

namespace AspNetMicroservices.Shared.Extensions.QueryableExtensions
{
	/// <summary>
	/// Queryable extensions.
	/// </summary>
	public static partial class QueryableExtensions
	{
		/// <summary>
		/// Sort the queryable sequence of type <see cref="TSource"/> by ascending or descending.
		/// </summary>
		/// <param name="source">Queryable sequence of objects of type <see cref="TSource"/>.</param>
		/// <param name="propertyName">Name of the property to sort by.</param>
		/// <param name="isDesc">Is descend sort direction.</param>
		/// <typeparam name="TSource">Type of object in queryable sequence.</typeparam>
		/// <returns>Result of sorting.</returns>
		public static IQueryable<TSource> TrySort<TSource>(
			this IQueryable<TSource> source, string propertyName, bool isDesc = false)
				=> isDesc ? source.OrderByDescending(propertyName) : source.OrderBy(propertyName);

		/// <summary>
		/// Sort the ordered queryable sequence of type <see cref="TSource"/> by ascending or descending.
		/// </summary>
		/// <param name="source">Queryable sequence of objects of type <see cref="TSource"/>.</param>
		/// <param name="propertyName">Name of the property to sort by.</param>
		/// <param name="isDesc">Is descend sort direction.</param>
		/// <typeparam name="TSource">Type of object in queryable sequence.</typeparam>
		/// <returns>Result of sorting.</returns>
		public static IQueryable<TSource> TrySortThen<TSource>(
			this IOrderedQueryable<TSource> source, string propertyName, bool isDesc = false)
				=> isDesc ? source.ThenByDescending(propertyName) : source.ThenBy(propertyName);
	}
}