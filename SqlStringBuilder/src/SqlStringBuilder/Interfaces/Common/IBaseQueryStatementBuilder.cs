namespace SqlStringBuilder.Interfaces.Common
{
    /// <summary>
    /// Base SQL statement builder.
    /// </summary>
    public interface IBaseQueryStatementBuilder
    {
	    /// <summary>
	    /// Build SQL statement.
	    /// </summary>
	    /// <returns></returns>
        string Build();

	    /// <summary>
	    /// Set raw SQL statement.
	    /// </summary>
	    /// <returns></returns>
        string RawSql(string sql);
    }
}