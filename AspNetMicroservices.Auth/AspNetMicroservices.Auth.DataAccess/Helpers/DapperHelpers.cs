using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;

using AspNetMicroservices.Auth.DataAccess.Mapping;
using AspNetMicroservices.Shared.Utils;

using Dapper;

namespace AspNetMicroservices.Auth.DataAccess.Helpers
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

        private static Func<Type, IEnumerable<PropertyInfo>, string, PropertyInfo> _propertySelectorCached = (type, properties, columnName)
            => properties
                .FirstOrDefault(x
                    => Utils.GetNameFromColumnAttribute(x) == columnName.ToLower());

        /// <summary>
        /// Create map from POCO model to table.
        /// </summary>
        /// <typeparam name="T">POCO type.</typeparam>
        public static void SetTypeMap<T>()
		{
			//var map = new CustomPropertyTypeMap(typeof(T), _propertySelector);
			var map = new CustomPropertyTypeMapWithCachedProperties(typeof(T), _propertySelectorCached);
            SqlMapper.SetTypeMap(typeof(T), map);
		}

        // TODO: add description. Check  multiple enumeration.
        public static IEnumerable<KeyValuePair<string, object>> BuildKeyValuePairsFromObject<TEntity>(
	        TEntity model, bool includePk = true)
        {
	        var dictionary = new Dictionary<string, object>();
	        var properties = typeof(TEntity).GetRuntimeProperties();
	        var primaryKey = properties.FirstOrDefault(x => x.GetCustomAttribute(typeof(KeyAttribute)) is KeyAttribute);

	        foreach (var property in properties)
	        {
				if (!includePk && string.Compare(property.Name, primaryKey?.Name, StringComparison.OrdinalIgnoreCase) == 0)
			       continue;

				var value = property.GetValue(model);

		        var colAttr = property.GetCustomAttribute(typeof(ColumnAttribute)) as ColumnAttribute;

		        if (colAttr is null)
			        continue;

				dictionary.Add(colAttr.Name, value);
	        }

	        return dictionary;
        }
	}
}