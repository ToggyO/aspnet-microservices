using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Reflection;

using AspNetMicroservices.Shared.Extensions.StringExtensions;

namespace AspNetMicroservices.Shared.Utils
{
	/// <summary>
	/// Represents methods, that helps to build SQL statement.
	/// </summary>
	public class SqlHelpers
	{
		/// <summary>
		/// Check if page index is valid.
		/// </summary>
		/// <param name="page">Page number.</param>
		/// <param name="pageSize">Page size.</param>
		/// <returns>Page index.</returns>
		public static int CreateOffset(int page, int pageSize)
		{
			int queryPage = (page - 1) * pageSize;
			return queryPage < 0 ? 0 : queryPage;
		}

		/// <summary>
		/// Matches between the class property name and the column name in the database table
		/// and returns the column name.
		/// </summary>
		/// <param name="propertyName">Name of property.</param>
		/// <typeparam name="TEntity">Type of entity, where property is declared.</typeparam>
		/// <returns></returns>
		public static string MapToTableColumnName<TEntity>(string propertyName) where TEntity : class
		{
			string name = propertyName.FirstLetterToUpper();

			var property = typeof(TEntity).GetProperty(name);
			if (property is null)
				return string.Empty;

			var colAttr = (ColumnAttribute)property.GetCustomAttribute(typeof(ColumnAttribute));
			if (colAttr is null)
				return string.Empty;

			return colAttr.Name;
		}

		/// <summary>
		/// Represents a class that flattens the differences between SQL syntax for different database management systemsю
		/// </summary>
		public class Adapter
		{
			private static readonly Dictionary<string, string> AdapterDictionary
				= new (6)
				{
					["sqlconnection"] = "; SELECT SCOPE_IDENTITY() @PkId ",
					["sqlceconnection"] = "; SELECT @@IDENTITY @PkId ",
					["npgsqlconnection"] = " RETURNING @PkId",
					["sqliteconnection"] = "; SELECT last_insert_rowid() @PkId",
					["mysqlconnection"] = "; SELECT LAST_INSERT_ID() @PkId",
					["fbconnection"] = ""
				};

			/// <summary>
			/// Appends SQL statement, that allows to retrieve last inserted id.
			/// </summary>
			/// <param name="connection">Instance of <see cref="IDbConnection"/>.</param>
			/// <param name="sql">SQL query.</param>
			/// <param name="pkColumnName">Primary key column name.</param>
			/// <returns></returns>
			public static string AppendReturningIdentity(IDbConnection connection,
				string sql, string pkColumnName = "id")
			{
				var name = connection.GetType().Name.ToLower();
				AdapterDictionary.TryGetValue(name, out string query);
				if (query is not null)
					sql += query.Replace("@PkId", pkColumnName);
				return sql;
			}
		}
	}
}