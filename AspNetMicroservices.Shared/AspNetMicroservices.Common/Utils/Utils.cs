using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace AspNetMicroservices.Common.Utils
{
	/// <summary>
	/// Set of utility methods.
	/// </summary>
	public static class Utils
	{
		/// <summary>
		/// Retrieve value of `Name` field of <see cref="ColumnAttribute"/>.
		/// </summary>
		/// <param name="propertyName">Name of property.</param>
		/// <typeparam name="TEntity">Entity type.</typeparam>
		/// <returns></returns>
		public static string GetNameFromColumnAttribute<TEntity>(string propertyName)
			where TEntity : new()
			=> GetNameFromColumnAttribute(typeof(TEntity).GetProperty(propertyName));

		/// <summary>
		/// Retrieve value of `Name` field of <see cref="ColumnAttribute"/>.
		/// </summary>
		/// <param name="member">Member info.</param>
		/// <returns></returns>
		public static string GetNameFromColumnAttribute(MemberInfo? member)
		{
			if (member is null)
				return String.Empty;

			var attribute = member.GetCustomAttribute<ColumnAttribute>();
			return (attribute?.Name ?? member.Name).ToLower();
		}

		/// <summary>
		/// Retrieve value of `Name` field of <see cref="TableAttribute"/>.
		/// </summary>
		/// <param name="toLower">Indicates, whether to return name in lower case.</param>
		/// <typeparam name="TEntity">Entity type.</typeparam>
		/// <returns></returns>
		public static string GetNameFromTableAttribute<TEntity>(bool toLower = false)
			where TEntity : new()
		{
			var attribute = typeof(TEntity).GetCustomAttribute<TableAttribute>();
			if (attribute is null)
				return String.Empty;

			if (toLower)
				return attribute.Name.ToLower();

			return attribute.Name;
		}

		/// <summary>
		/// Generates stringed guid value in provided format.
		/// </summary>
		/// <param name="format">String guid format.</param>
		/// <returns></returns>
		public static string GenerateGuidString(string format = "N")
			=> Guid.NewGuid().ToString(format);

		/// <summary>
		/// Creates cached value key.
		/// </summary>
		/// <param name="prefix">Key prefix.</param>
		/// <param name="body">Key main part.</param>
		/// <returns></returns>
		public static string CreateCacheKey(string prefix, string body) => $"{prefix}::{body}";
	}
}