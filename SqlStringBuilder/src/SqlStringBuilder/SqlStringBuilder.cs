using SqlStringBuilder.Interfaces;
using SqlStringBuilder.Internal;

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
        public static ISelectQueryStatementBuilder Select() => SelectQueryStatementBuilder.Create();

        /// <summary>
        /// Initialize new instance of <see cref="IInsertQueryStatementBuilder"/>.
        /// </summary>
        /// <returns>Insert query statement builder.</returns>
        public static IInsertQueryStatementBuilder Insert() => InsertQueryStatementBuilder.Create();

        /// <summary>
        /// Initialize new instance of <see cref="IUpdateQueryStatementBuilder"/>.
        /// </summary>
        /// <returns>Update query statement builder.</returns>
        public static IUpdateQueryStatementBuilder Update() => UpdateQueryStatementBuilder.Create();

        /// <summary>
        /// Initialize new instance of <see cref="IDeleteQueryStatementBuilder"/>.
        /// </summary>
        /// <returns>Delete query statement builder.</returns>
        public static IDeleteQueryStatementBuilder Delete() => DeleteQueryStatementBuilder.Create();
    }
}