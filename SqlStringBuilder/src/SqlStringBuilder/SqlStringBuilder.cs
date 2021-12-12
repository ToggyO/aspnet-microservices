using SqlStringBuilder.Interfaces.Common;
using SqlStringBuilder.Interfaces.Select;
using SqlStringBuilder.Internal;
using SqlStringBuilder.Internal.Select;

namespace SqlStringBuilder
{
    /// <summary>
    /// SQL statement string builder.
    /// </summary>
    public static class SqlStringBuilder
    {
        /// <summary>
        /// Initialize new instance of <see cref="ISelectQueryStatementBuilder"/>.
        /// </summary>
        /// <returns>Select query statement builder.</returns>
        public static ISelectQueryStatementBuilder CreateSelectStatement() => SelectQueryStatementBuilder.Create();

        /// <summary>
        /// Initialize new instance of <see cref="IInsertQueryStatementBuilder"/>.
        /// </summary>
        /// <returns>Insert query statement builder.</returns>
        public static IInsertQueryStatementBuilder CreateInsertStatement() => InsertQueryStatementBuilder.Create();

        /// <summary>
        /// Initialize new instance of <see cref="IUpdateQueryStatementBuilder"/>.
        /// </summary>
        /// <returns>Update query statement builder.</returns>
        public static IUpdateQueryStatementBuilder CreateUpdateStatement() => UpdateQueryStatementBuilder.Create();

        /// <summary>
        /// Initialize new instance of <see cref="IDeleteQueryStatementBuilder"/>.
        /// </summary>
        /// <returns>Delete query statement builder.</returns>
        public static IDeleteQueryStatementBuilder CreateDeleteStatement() => DeleteQueryStatementBuilder.Create();
    }
}