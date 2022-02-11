using AspNetMicroservices.Auth.DataAccess.Helpers;
using AspNetMicroservices.Auth.Domain.Models.Database.Users;
using AspNetMicroservices.Data;

using SqlKata;

namespace AspNetMicroservices.Auth.DataAccess.Extensions
{
	/// <summary>
	/// Extensions for <see cref="https://github.com/sqlkata/querybuilder"/>.
	/// </summary>
	public static class SqlKataExtensions
	{
		/// <summary>
		/// Creates "INSERT" SQL statement and maps property names of object with parameters
		/// to its representations from <see cref="ColumnAttribute"/>.
		/// </summary>
		/// <param name="query">Instance of <see cref="SqlKata.Query"/>.</param>
		/// <param name="entity">Object with parameter values.</param>
		/// <param name="returningId">Indicates whether to return last inserted id.</param>
		/// <typeparam name="TEntity">Type of parameter object.</typeparam>
		/// <returns></returns>
		public static Query AsInsertWithMapping<TEntity>(this Query query,
			TEntity entity, bool returningId = false) where TEntity : class, new ()
		{
			query.AsInsert(DapperHelpers.BuildKeyValuePairsFromObject(entity, false), returningId);
			return query;
		}

		/// <summary>
		/// Creates "order" clause SQL statement by provided column name and order direction.
		/// </summary>
		/// <param name="query">Instance of <see cref="SqlKata.Query"/>.</param>
		/// <param name="columnName">Column name.</param>
		/// <param name="isDesc">Order direction.</param>
		/// <returns></returns>
		public static Query OrderByWithMapping(this Query query,
			string columnName, bool isDesc = false)
		{
			if (columnName is not null)
			{
				var normalizedColumnNameToOrderBy = SqlHelpers.MapToTableColumnName<UserModel>(columnName);
				if (isDesc)
					query.OrderByDesc(normalizedColumnNameToOrderBy);
				else
					query.OrderBy(normalizedColumnNameToOrderBy);
			}

			return query;
		}
	}
}