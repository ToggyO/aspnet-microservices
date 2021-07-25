using System;
using System.Linq;
using System.Linq.Expressions;

using AspNetMicroservices.Shared.Contracts;

namespace AspNetMicroservices.Shared.Extensions
{
	/// <summary>
	/// Queryable extensions.
	/// </summary>
	public static class QueryableExtensions
	{
		public static IOrderedQueryable<TSource> OrderBy<TSource>(this IQueryable<TSource> source, string property, bool isDesc)
		{
			return ApplyOrder(source, property, isDesc ? "OrderByDescending" : "OrderBy");
		}

		// TODO: add description
		/// <summary>
		///
		/// </summary>
		/// <param name="source"></param>
		/// <param name="propertyName"></param>
		/// <param name="methodName"></param>
		/// <typeparam name="TSource"></typeparam>
		/// <returns></returns>
		public static IOrderedQueryable<TSource> ApplyOrder<TSource>(this IQueryable<TSource> source,
			string propertyName, string methodName)
		{
			string[] props = propertyName.Split('.');
			var type = typeof(TSource);
			var arg = Expression.Parameter(type, "x");
			Expression exp = arg;

			foreach (var prop in props)
			{
				var pi = type.GetProperty(prop);
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