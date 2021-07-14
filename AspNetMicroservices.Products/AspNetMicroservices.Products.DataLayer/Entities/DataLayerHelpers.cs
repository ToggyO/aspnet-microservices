using System.Reflection;

using LinqToDB.Mapping;

namespace AspNetMicroservices.Products.DataLayer.Entities
{
	/// <summary>
	/// Represents custom methods, that takes data from <see cref="LinqToDB"/> attributes.
	/// </summary>
	public static class DataLayerHelpers
	{
		/// <summary>
		/// Retrieves `Name` field from <see cref="TableAttribute"/>.
		/// </summary>
		/// <typeparam name="TEntity">Linq2Db entity class, inherits from <see cref="BaseEntity"/>.</typeparam>
		/// <returns>Table name in database.</returns>
		public static string ExtractTableName<TEntity>() where TEntity : BaseEntity
		{
			TableAttribute attribute = (TableAttribute)typeof(TEntity).GetCustomAttribute(typeof(TableAttribute));
			if (attribute is null)
				return null;

			return attribute.Name;
		}

		/// <summary>
		/// Retrieves `Name` field from <see cref="ColumnAttribute"/>.
		/// </summary>
		/// <param name="propertyName">Name of property in entity class.</param>
		/// <typeparam name="TEntity">Linq2Db entity class, inherits from <see cref="BaseEntity"/>.</typeparam>
		/// <returns>Column name in database.</returns>
		public static string ExtractTableColumnName<TEntity>(string propertyName) where TEntity : BaseEntity
		{
			PropertyInfo property = typeof(TEntity).GetProperty(propertyName);
			if (property is null)
				return null;

			ColumnAttribute attribute = (ColumnAttribute)property.GetCustomAttribute(typeof(ColumnAttribute));
			if (attribute is null)
				return null;

			return attribute.Name;
		}
	}
}