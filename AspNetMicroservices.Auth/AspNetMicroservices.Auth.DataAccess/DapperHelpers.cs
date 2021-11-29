using System;
using System.Linq;
using System.Reflection;

using AspNetMicroservices.Shared.Utils;

using Dapper;

namespace AspNetMicroservices.Auth.DataAccess
{
	/// <summary>
	/// Dapper helper methods.
	/// </summary>
	public static class DapperHelpers
	{
		private static Func<Type, string, PropertyInfo> _propertySelector = (type, columnName)
			=> type.GetProperties()
				.FirstOrDefault(x
					=> Utils.GetNameFromColumnAttribute(x) == columnName.ToLower());

		/// <summary>
		///
		/// </summary>
		/// <typeparam name="T"></typeparam>
		public static void SetTypeMap<T>()
		{
			var map = new CustomPropertyTypeMap(typeof(T), _propertySelector);
			SqlMapper.SetTypeMap(typeof(T), map);
		}
	}
}