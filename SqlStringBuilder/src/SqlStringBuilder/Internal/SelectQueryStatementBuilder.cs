using System.Collections.Generic;
using SqlStringBuilder.Common;
using SqlStringBuilder.Interfaces;

namespace SqlStringBuilder.Internal
{
    internal sealed class SelectQueryStatementBuilder
        : BaseQueryStatementBuilder, ISelectQueryStatementBuilder
    {
        private readonly Stack<FromTableParameter> _fromTables = new ();

        private readonly Stack<WithColumnsParameter> _withColumns = new ();

        private SelectQueryStatementBuilder() {}

        public static ISelectQueryStatementBuilder Create()
        {
            return new SelectQueryStatementBuilder();
        }

        public ISelectQueryStatementBuilder FromTable(string tableName)
            => FromTable(new FromTableParameter
            {
                Name = tableName,
                Alias = ExtractAlias(tableName)
            });

        public ISelectQueryStatementBuilder FromTable(FromTableParameter parameter)
        {
            _fromTables.Push(parameter);
            return this;
        }

        public ISelectQueryStatementBuilder WithColumns(string[] columns)
        {
            throw new System.NotImplementedException();
        }

    }
}