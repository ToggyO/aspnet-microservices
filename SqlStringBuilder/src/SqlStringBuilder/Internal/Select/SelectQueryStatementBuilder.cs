using SqlStringBuilder.Interfaces.Select;
using SqlStringBuilder.Internal.BaseQuery;
using SqlStringBuilder.Internal.Components;
using SqlStringBuilder.Internal.Constants;

namespace SqlStringBuilder.Internal.Select
{
    internal sealed class SelectQueryStatementBuilder
        : BaseQueryStatementBuilder<SelectQueryStatementBuilder>, ISelectQueryStatementBuilder
    {
        private SelectQueryStatementBuilder() {}

        public static ISelectQueryStatementBuilder Create()
        {
            return new SelectQueryStatementBuilder();
        }

        public ISelectQueryStatementBuilder From(string tableName, string alias = null)
            => AddOrReplaceComponent(ComponentTypes.From, new FromComponent
            {
                Table = tableName,
                Alias = alias,
            });

        public ISelectQueryStatementBuilder Select(string[] columns)
        {
            throw new System.NotImplementedException();
        }
    }
}