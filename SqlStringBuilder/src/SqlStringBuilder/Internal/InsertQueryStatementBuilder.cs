using SqlStringBuilder.Interfaces;

namespace SqlStringBuilder.Internal
{
    internal sealed class InsertQueryStatementBuilder : IInsertQueryStatementBuilder
    {
        private InsertQueryStatementBuilder() {}

        public static IInsertQueryStatementBuilder Create()
        {
            return new InsertQueryStatementBuilder();
        }
    }
}