using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Reflection;

using AspNetMicroservices.Shared.Extensions;

namespace AspNetMicroservices.Shared.Utils
{
	// TODO: add description
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

		// TODO: add description.
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

		// TODO: add description.
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