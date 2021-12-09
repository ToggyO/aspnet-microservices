using SqlStringBuilder.Interfaces.Common;
using SqlStringBuilder.Internal.Components;
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
			if (value is null)
				return Not(op != ComparisonOperators.Equal).WhereNull(columnName);

			// TODO: handle boolean value

			return AddComponent(ComponentTypes.Where, new BasicCondition<TValue>
			{
				Column = columnName,
				Operator = op,
				Value = value,
				IsOr = GetOr(),
				IsNot = GetNot(),
			});
		}

		/// <inheritdoc cref="IWhereQueryStatementBuilder.OrWhere{TValue}(string, TValue)"/>.
		public IWhereQueryStatementBuilder OrWhere<TValue>(string columnName, TValue value)
			=> OrWhere(columnName, ComparisonOperators.Equal, value);

		/// <inheritdoc cref="IWhereQueryStatementBuilder.OrWhere{TValue}(string, string, TValue)"/>.
		public IWhereQueryStatementBuilder OrWhere<TValue>(string columnName, string op, TValue value) => Or().Where(columnName, op, value);

		/// <inheritdoc cref="IWhereQueryStatementBuilder.WhereNot{TValue}(string, TValue)"/>.
		public IWhereQueryStatementBuilder WhereNot<TValue>(string columnName, TValue value)
			=> WhereNot(columnName, ComparisonOperators.Equal, value);

		/// <inheritdoc cref="IWhereQueryStatementBuilder.WhereNot{TValue}(string, string, TValue)"/>.
		public IWhereQueryStatementBuilder WhereNot<TValue>(string columnName, string op, TValue value) => Not().Where(columnName, op, value);

		/// <inheritdoc cref="IWhereQueryStatementBuilder.OrWhereNot{TValue}(string, TValue)"/>.
		public IWhereQueryStatementBuilder OrWhereNot<TValue>(string columnName, TValue value)
			=> OrWhereNot(columnName, ComparisonOperators.Equal, value);

		/// <inheritdoc cref="IWhereQueryStatementBuilder.OrWhereNot{TValue}(string, string, TValue)"/>.
		public IWhereQueryStatementBuilder OrWhereNot<TValue>(string columnName, string op, TValue value)
			=> Not().Or().Where(columnName, op, value);

		/// <inheritdoc cref="IWhereQueryStatementBuilder.WhereNull(string)"/>.
		public IWhereQueryStatementBuilder WhereNull(string columnName)
			=> AddComponent(ComponentTypes.Where, new NullCondition
			{
				Column = columnName
			});

		/// <inheritdoc cref="IWhereQueryStatementBuilder.OrWhereNull(string)"/>.
		public IWhereQueryStatementBuilder OrWhereNull(string columnName) => Or().WhereNull(columnName);
	}
}