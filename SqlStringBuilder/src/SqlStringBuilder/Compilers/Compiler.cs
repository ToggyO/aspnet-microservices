using System;

using SqlStringBuilder.Common;
using SqlStringBuilder.Interfaces.Common;
using SqlStringBuilder.Interfaces.Select;
using SqlStringBuilder.Internal;
using SqlStringBuilder.Internal.Components;
using SqlStringBuilder.Internal.Constants;
using SqlStringBuilder.Internal.Enums;

namespace SqlStringBuilder.Compilers
{
	/// <summary>
	/// Basic SQL compiler class.
	/// </summary>
	public partial class Compiler
	{
		public virtual SqlResult Compile(IBaseQueryStatementBuilder builder)
		{
			return builder.QueryType switch
			{
				SqlStatementTypes.Select => CompileSelectStatement((ISelectQueryStatementBuilder)builder),
				SqlStatementTypes.Insert => CompileInsertStatement((IInsertQueryStatementBuilder)builder),
				SqlStatementTypes.Update => CompileUpdateStatement((IUpdateQueryStatementBuilder)builder),
				SqlStatementTypes.Delete => CompileDeleteStatement((IDeleteQueryStatementBuilder)builder),
			};
		}

		internal virtual SqlResult CompileSelectStatement(ISelectQueryStatementBuilder builder)
		{
			var ctx = new CompilationContext<ISelectQueryStatementBuilder>(
				(IInternalBaseQueryStatementBuilder<ISelectQueryStatementBuilder>) builder);

			var results = new[]
			{
				CompileSelect(ctx),
				CompileFrom(ctx),
				CompileWheres(ctx),
			};

			string sql = string.Join(" ", results);
			return new SqlResult { Sql = sql };
		}

		internal virtual SqlResult CompileInsertStatement(IInsertQueryStatementBuilder builder)
		{
			return new SqlResult();
		}

		internal virtual SqlResult CompileUpdateStatement(IUpdateQueryStatementBuilder builder)
		{
			return new SqlResult();
		}

		internal virtual SqlResult CompileDeleteStatement(IDeleteQueryStatementBuilder builder)
		{
			return new SqlResult();
		}

		internal virtual string CompileSelect<TQuery>(CompilationContext<TQuery> ctx)
			where TQuery : IBaseQueryStatementBuilder
		{
			// TODO: add aggregated columns


			return $"SELECT ";
		}

		internal virtual string CompileFrom<TQuery>(CompilationContext<TQuery> ctx)
			where TQuery : IBaseQueryStatementBuilder
		{
			if (!ctx.Builder.HasComponent<AbstractFrom>(ComponentTypes.From))
				throw new InvalidOperationException("No table is set");

			var fromComponent = ctx.Builder.GetComponent<AbstractFrom>(ComponentTypes.From);
			return $"FROM {fromComponent.Table} AS {fromComponent.Alias}";
		}

		internal virtual string CompileWheres<TQuery>(CompilationContext<TQuery> ctx)
			where TQuery : IBaseQueryStatementBuilder
		{
			if (!ctx.Builder.HasComponent<AbstractFrom>(ComponentTypes.From) ||
			    !ctx.Builder.HasComponent<AbstractCondition>(ComponentTypes.Where))
				return null;

			var conditions = ctx.Builder.GetComponents<AbstractCondition>(ComponentTypes.Where);
			string sql = "";

			return string.IsNullOrEmpty(sql) ? null : $"WHERE {sql}";
		}
	}
}