using System.Collections.Generic;

using SqlStringBuilder.Interfaces.Common;
using SqlStringBuilder.Internal.Components;

namespace SqlStringBuilder.Internal
{
	/// <summary>
	/// Represents abstract SQL statement builder
	/// </summary>
	internal abstract class AbstractQueryStatementBuilder : IBaseQueryStatementBuilder
	{
		internal readonly LinkedList<AbstractComponent> Components = new ();

		/// <inheritdoc cref="IBaseQueryStatementBuilder.Build"/>.
		public string Build()
		{
			return string.Empty;
		}

		/// <inheritdoc cref="IBaseQueryStatementBuilder.RawSql"/>.
		public string RawSql(string sql)
		{
			return string.Empty;
		}
	}
}