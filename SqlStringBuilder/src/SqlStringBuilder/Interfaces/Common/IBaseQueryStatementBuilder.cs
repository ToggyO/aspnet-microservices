using SqlStringBuilder.Common;
using SqlStringBuilder.Internal.Enums;

namespace SqlStringBuilder.Interfaces.Common
{
    /// <summary>
    /// Base SQL statement builder.
    /// </summary>
    public interface IBaseQueryStatementBuilder
    {
	    /// <summary>
	    /// SQL statement type.
	    /// </summary>
	    SqlStatementTypes QueryType { get; init; }

	    /// <summary>
	    /// Build SQL statement.
	    /// </summary>
	    /// <returns></returns>
	    SqlResult Build();

	    /// <summary>
	    /// Set raw SQL statement.
	    /// </summary>
	    /// <returns></returns>
        string RawSql(string sql);
    }
}