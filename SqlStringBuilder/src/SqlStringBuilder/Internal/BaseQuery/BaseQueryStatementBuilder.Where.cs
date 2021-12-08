using SqlStringBuilder.Interfaces.Common;
using SqlStringBuilder.Internal.Constants;

namespace SqlStringBuilder.Internal.BaseQuery
{
	/// <inheritdoc cref="IBaseQueryStatementBuilder"/>.
	internal abstract partial class BaseQueryStatementBuilder<TQuery> : IWhereQueryStatementBuilder
	{
		/// <inheritdoc cref="IWhereQueryStatementBuilder.Where{TValue}(string, TValue)"/>.
		public IWhereQueryStatementBuilder Where<TValue>(string columnName, TValue value)
			=> Where(columnName, ComparisonOperators.Equal, value);

		/// <inheritdoc cref="IWhereQueryStatementBuilder.Where{TValue}(string, string, TValue)"/>.
		public IWhereQueryStatementBuilder Where<TValue>(string columnName, string op, TValue value)
		{
			throw new System.NotImplementedException();
		}

		/// <inheritdoc cref="IWhereQueryStatementBuilder.OrWhere{TValue}(string, TValue)"/>.
		public IWhereQueryStatementBuilder OrWhere<TValue>(string columnName, TValue value)
			=> OrWhere(columnName, ComparisonOperators.Equal, value);

		/// <inheritdoc cref="IWhereQueryStatementBuilder.OrWhere{TValue}(string, string, TValue)"/>.
		public IWhereQueryStatementBuilder OrWhere<TValue>(string columnName, string op, TValue value)
		{
			throw new System.NotImplementedException();
		}
	}
}