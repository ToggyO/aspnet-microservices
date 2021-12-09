using SqlStringBuilder.Interfaces.Common;
using SqlStringBuilder.Internal.BaseQuery;
using SqlStringBuilder.Internal.Enums;

namespace SqlStringBuilder.Internal
{
    internal sealed class DeleteQueryStatementBuilder : BaseQueryStatementBuilder<DeleteQueryStatementBuilder>,
	    IDeleteQueryStatementBuilder
    {
	    private DeleteQueryStatementBuilder() => QueryType = SqlStatementTypes.Delete;

        public static IDeleteQueryStatementBuilder Create()
        {
            return new DeleteQueryStatementBuilder();
        }
    }
}