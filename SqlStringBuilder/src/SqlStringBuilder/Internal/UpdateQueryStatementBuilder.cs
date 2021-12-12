using SqlStringBuilder.Interfaces.Common;
using SqlStringBuilder.Internal.BaseQuery;
using SqlStringBuilder.Internal.Enums;

namespace SqlStringBuilder.Internal
{
    internal sealed class UpdateQueryStatementBuilder : BaseQueryStatementBuilder<UpdateQueryStatementBuilder>,
        IUpdateQueryStatementBuilder
    {
        private UpdateQueryStatementBuilder() => QueryType = SqlStatementTypes.Update;

        public static IUpdateQueryStatementBuilder Create()
        {
            return new UpdateQueryStatementBuilder();
        }
    }
}