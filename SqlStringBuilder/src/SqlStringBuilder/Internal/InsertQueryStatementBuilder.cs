using SqlStringBuilder.Interfaces;
using SqlStringBuilder.Interfaces.Common;

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