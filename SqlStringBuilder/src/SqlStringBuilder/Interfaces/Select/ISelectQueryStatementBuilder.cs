using SqlStringBuilder.Interfaces.Common;

namespace SqlStringBuilder.Interfaces.Select
{
	/// <summary>
	/// SQL SELECT statement builder.
	/// </summary>
    public interface ISelectQueryStatementBuilder : IWhereQueryStatementBuilder
    {
	    /// <summary>
	    /// Gets a value that indicates whether the "DISTINCT" keyword is required in SQL statement.
	    /// </summary>
	    bool IsDistinct { get; }

	    /// <summary>
	    /// Add a "from" Component.
	    /// </summary>
	    /// <param name="tableName">Table name.</param>
	    /// <param name="alias">Table alias.</param>
	    /// <returns></returns>
        ISelectQueryStatementBuilder From(string tableName, string alias = null);

	    /// <summary>
	    /// Add a "select" Component.
	    /// </summary>
	    /// <param name="columns">Set of column names.</param>
	    /// <returns></returns>
        ISelectQueryStatementBuilder Select(params string[] columns);

	    /// <summary>
	    /// Sets the identifier of the distinct keyword to true.
	    /// </summary>
	    /// <returns></returns>
        ISelectQueryStatementBuilder Distinct();
    }
}