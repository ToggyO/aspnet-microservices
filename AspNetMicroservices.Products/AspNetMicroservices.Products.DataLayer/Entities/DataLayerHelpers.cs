using System.Reflection;

using LinqToDB.Mapping;

namespace AspNetMicroservices.Products.DataLayer.Entities
{
	// TODO: add summary
	/// <summary>
	/// Represents
	/// </summary>
	public static class DataLayerHelpers
	{
		public static string ExtractTableName<TEntity>() where TEntity : BaseEntity
		{
			TableAttribute attribute = (TableAttribute)typeof(TEntity).GetCustomAttribute(typeof(TableAttribute));
			if (attribute is null)
				return null;

			return attribute.Name;
		}

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