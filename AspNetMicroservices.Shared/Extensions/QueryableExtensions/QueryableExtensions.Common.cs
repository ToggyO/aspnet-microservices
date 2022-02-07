using System;
using System.Linq;
using System.Linq.Expressions;

using AspNetMicroservices.Shared.Extensions.StringExtensions;

namespace AspNetMicroservices.Shared.Extensions.QueryableExtensions
{
	/// <summary>
	/// Queryable extensions.
	/// </summary>
	public static partial class QueryableExtensions
	{
		/// <summary>
		///	Sort the queryable sequence of type <see cref="TSource"/> by ascending.
		/// </summary>
		/// <param name="source">Queryable sequence of objects of type <see cref="TSource"/>.</param>
		/// <param name="propertyName">Name of the property to sort by.</param>
		/// <typeparam name="TSource">Type of object in queryable sequence.</typeparam>
		/// <returns>Result of sorting.</returns>
		public static IOrderedQueryable<TSource> OrderBy<TSource>(this IQueryable<TSource> source, string propertyName)
			=> ApplyOrder(source, propertyName, "OrderBy");

		/// <summary>
		///	Sort the queryable sequence of type <see cref="TSource"/> by descending.
		/// </summary>
		/// <param name="source">Queryable sequence of objects of type <see cref="TSource"/>.</param>
		/// <param name="propertyName">Name of the property to sort by.</param>
		/// <typeparam name="TSource">Type of object in queryable sequence.</typeparam>
		/// <returns>Result of sorting.</returns>
		public static IOrderedQueryable<TSource> OrderByDescending<TSource>(this IQueryable<TSource> source, string propertyName)
			=> ApplyOrder(source, propertyName, "OrderByDescending");

		/// <summary>
		///	Sort the ordered queryable sequence of type <see cref="TSource"/> by ascending.
		/// </summary>
		/// <param name="source">Queryable sequence of objects of type <see cref="TSource"/>.</param>
		/// <param name="propertyName">Name of the property to sort by.</param>
		/// <typeparam name="TSource">Type of object in queryable sequence.</typeparam>
		/// <returns>Result of sorting.</returns>
		public static IOrderedQueryable<TSource> ThenBy<TSource>(this IOrderedQueryable<TSource> source, string propertyName)
			=> ApplyOrder(source, propertyName, "ThenBy");

		/// <summary>
		///	Sort the ordered queryable sequence of type <see cref="TSource"/> by descending.
		/// </summary>
		/// <param name="source">Queryable sequence of objects of type <see cref="TSource"/>.</param>
		/// <param name="propertyName">Name of the property to sort by.</param>
		/// <typeparam name="TSource">Type of object in queryable sequence.</typeparam>
		/// <returns>Result of sorting.</returns>
		public static IOrderedQueryable<TSource> ThenByDescending<TSource>(this IOrderedQueryable<TSource> source, string propertyName)
			=> ApplyOrder(source, propertyName, "ThenByDescending");


		/// <summary>
		///	Creates lambda expression, that returns key of property with name <see cref="propertyName"/> from <see cref="TSource"/> object
		/// and executes sort method from <see cref="Queryable"/>, specified by name.
		/// </summary>
		/// <param name="source">Queryable sequence of objects of type <see cref="TSource"/>.</param>
		/// <param name="propertyName">Name of the property, that needs to retrieve from <see cref="TSource"/>.</param>
		/// <param name="methodName">Name of method to execute.</param>
		/// <typeparam name="TSource">Type of object in queryable sequence.</typeparam>
		/// <returns>Result of sorting.</returns>
		private static IOrderedQueryable<TSource> ApplyOrder<TSource>(this IQueryable<TSource> source,
			string propertyName, string methodName)
		{
			string[] props = propertyName.Split('.');
			var type = typeof(TSource);
			var arg = Expression.Parameter(type, "x");
			Expression exp = arg;

			foreach (var prop in props)
			{
				var pi = type.GetProperty(prop.FirstLetterToUpper());
				if (pi is null)
					continue;
				exp = Expression.Property(exp, pi);
				type = pi.PropertyType;
			}

			var delegateType = typeof(Func<,>).MakeGenericType(typeof(TSource), type);
			var lambda = Expression.Lambda(delegateType, exp, arg);

			object result = typeof(Queryable).GetMethods().Single(
					method => method.Name == methodName
					          && method.IsGenericMethodDefinition
					          && method.GetGenericArguments().Length == 2
					          && method.GetParameters().Length == 2)
				.MakeGenericMethod(typeof(TSource), type)
				.Invoke(null, new object[] { source, lambda });
			return (IOrderedQueryable<TSource>) result;
		}
	}
}