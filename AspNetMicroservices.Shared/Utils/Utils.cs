using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace AspNetMicroservices.Shared.Utils
{
	/// <summary>
	/// Set of utility methods.
	/// </summary>
	public static class Utils
	{
		/// <summary>
		/// Retrieve value of `Name` field of <see cref="ColumnAttribute"/>.
		/// </summary>
		/// <param name="member">Member info.</param>
		/// <returns></returns>
		public static string GetNameFromColumnAttribute(MemberInfo member)
		{
			if (member is null)
				return null;

			var attribute = member.GetCustomAttribute<ColumnAttribute>();
			return (attribute?.Name ?? member.Name).ToLower();
		}
	}
}