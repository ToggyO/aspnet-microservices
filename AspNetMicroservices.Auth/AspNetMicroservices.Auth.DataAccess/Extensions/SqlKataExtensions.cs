using AspNetMicroservices.Auth.DataAccess.Helpers;
using AspNetMicroservices.Auth.Domain.Models.Database.Users;
using AspNetMicroservices.Shared.Utils;

using SqlKata;

namespace AspNetMicroservices.Auth.DataAccess.Extensions
{
	// TODO: add description
	public static class SqlKataExtensions
	{
		public static Query AsInsertWithMapping<TEntity>(this Query query,
			TEntity entity, bool returningId = false) where TEntity : class, new ()
		{
			query.AsInsert(DapperHelpers.BuildKeyValuePairsFromObject(entity, false), returningId);
			return query;
		}

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