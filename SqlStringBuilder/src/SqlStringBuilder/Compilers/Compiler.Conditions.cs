using System.Collections.Generic;
using System.Linq;

using SqlStringBuilder.Interfaces.Common;
using SqlStringBuilder.Internal;
using SqlStringBuilder.Internal.Components;
using SqlStringBuilder.Internal.Constants;

namespace SqlStringBuilder.Compilers
{
	/// <summary>
	/// Basic SQL compiler class.
	/// </summary>
	public partial class Compiler
	{
		/// <summary>
		/// A list of white-listed operators
		/// </summary>
		/// <value></value>
		protected readonly HashSet<string> Operators =
			new(ComparisonOperators.ToList().Concat(DbComparisonOperators.ToList()));

		internal virtual string CompileConditions<TQuery>(List<AbstractCondition> conditions)
			where TQuery : IBaseQueryStatementBuilder
		{
			var result = new List<string>(conditions.Count);

			for (int i = 0; i < conditions.Count; i++)
			{
				string compiled = CompileCondition(conditions[i]);
				if (string.IsNullOrEmpty(compiled))
					continue;

				string boolOperator = i == 0 ? string.Empty : (conditions[i].IsOr ? OrIdentifier : AndIdentifier);
				result.Add(boolOperator + compiled);
			}

			return string.Join(" ", result);
		}

		internal virtual string CompileCondition(AbstractCondition condition)
		{
			// TODO: Use reflection to define method for condition compiling.
			// TODO: Check if genetic type parameter is necessary for "BasicCondition".
			if (condition is BasicCondition<object>)
			{
				return CompileBasicCondition((BasicCondition<object>)condition);
			}

			return string.Empty;
		}

		internal virtual string CompileBasicCondition<TValue>(BasicCondition<TValue> condition)
		{
			// TODO: parametrize value
			var sql = $"{condition.Column} {CheckOperator(condition.Operator)} {condition.Value}";

			if (condition.IsNot)
				sql = $"NOT ({sql})";

			return sql;
		}
	}
}