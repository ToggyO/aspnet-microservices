using SqlStringBuilder.Interfaces.Common;
using SqlStringBuilder.Internal.BaseQuery;
using SqlStringBuilder.Internal.Enums;

namespace SqlStringBuilder.Internal
{
    internal sealed class InsertQueryStatementBuilder : BaseQueryStatementBuilder<InsertQueryStatementBuilder>,
        IInsertQueryStatementBuilder
    {
        private InsertQueryStatementBuilder() => QueryType = SqlStatementTypes.Insert;

        public static IInsertQueryStatementBuilder Create()
        {
            return new InsertQueryStatementBuilder();
        }
    }
}