namespace SqlStringBuilder.Interfaces.Common
{
    public interface IWhereQueryStatementBuilder : IBaseQueryStatementBuilder
    {
	    /// <summary>
	    /// Perform a sub query where clause.
	    /// </summary>
	    /// <param name="columnName">Column name.</param>
	    /// <param name="value">Value to compare with.</param>
	    /// <typeparam name="TValue">Type of value.</typeparam>
	    /// <returns></returns>
        IWhereQueryStatementBuilder Where<TValue>(string columnName, TValue value);

	    /// <summary>
	    /// Perform a sub query where clause.
	    /// </summary>
	    /// <param name="columnName">Column name.</param>
	    /// <param name="op">Comparison operator.</param>
	    /// <param name="value">Value to compare with.</param>
	    /// <typeparam name="TValue">Type of value.</typeparam>
	    /// <returns></returns>
        IWhereQueryStatementBuilder Where<TValue>(string columnName, string op, TValue value);

	    /// <summary>
	    /// Perform a sub query where clause with as "OR".
	    /// </summary>
	    /// <param name="columnName">Column name.</param>
	    /// <param name="value">Value to compare with.</param>
	    /// <typeparam name="TValue">Type of value.</typeparam>
	    /// <returns></returns>
        IWhereQueryStatementBuilder OrWhere<TValue>(string columnName, TValue value);

	    /// <summary>
	    /// Perform a sub query where clause with as "OR".
	    /// </summary>
	    /// <param name="columnName">Column name.</param>
	    /// <param name="op">Comparison operator.</param>
	    /// <param name="value">Value to compare with.</param>
	    /// <typeparam name="TValue">Type of value.</typeparam>
	    /// <returns></returns>
        IWhereQueryStatementBuilder OrWhere<TValue>(string columnName, string op, TValue value);
    }
}